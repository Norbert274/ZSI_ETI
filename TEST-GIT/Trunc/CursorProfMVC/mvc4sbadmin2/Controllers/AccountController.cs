using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using nclprospekt.Models;
using nclprospekt.Exceptions;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using nclprospekt.Objects;
using System.Web.WebPages;

namespace nclprospekt.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public ActionResult Login(string returnUrl = "", string viewMsg = "")
        {

            //czy viewMsg nalezy do listy zatwierdzonych bledow
            if (viewMsg != "")
            { ViewBag.Info = viewMsg; }
              
            switch (viewMsg)
            {
                case "Twoja sesja została zakończona.":
                     break;
                 default:
                     viewMsg = "";
                     break;
            }
            

            if (User.Identity.IsAuthenticated)
            {
                if (returnUrl != null && returnUrl.Trim() != "")
                {
                    string saveTempData = "";
                    
                    if(TempData["msg"] != null)
                    {
                        saveTempData= TempData["msg"].ToString();
                    
                    }

                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    
                    FormsAuthentication.SignOut();

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 0 && returnUrl.StartsWith("/")
                           && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        return RedirectToAction("Login", "Account", new { @returnUrl = returnUrl, @viewMsg = saveTempData });
                    else
                        return LogOff();
                }
                else
                    return LogOff();
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl; 
                ViewBag.Message = viewMsg;
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
      //  [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {

            Session.Clear();
            //Session.Abandon();
            Session.RemoveAll();


            //Czyszczenie Cache - inaczej uprawnienia zostawaly na zbyt dlugo
            var enumerator = HttpRuntime.Cache.GetEnumerator();
                Dictionary<string, object> cacheItems = new Dictionary<string, object>();
              
            while (enumerator.MoveNext())
                    cacheItems.Add(enumerator.Key.ToString(), enumerator.Value);
               
            foreach (string key in cacheItems.Keys)
            {
                if (key.Contains("UserRoles_"))
                {
                    HttpRuntime.Cache.Remove(key);
                };
            }

            
            if (ModelState.IsValid) 
            {
                SecurityToken s = null;

                try
                {
                   s = ValidateLogOn(model.UserName, model.Password);
                
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }

               if (s !=null && s.token != null)
               {
                   
                   bool flag = (model.RememberMe == true ? true : false);

                   int version = 1;
                   DateTime now = DateTime.Now;

                   // respect to the `timeout` in Web.config.
                   TimeSpan timeout = FormsAuthentication.Timeout;
                   DateTime expire = now.Add(timeout);


                       // generate authentication ticket
                       System.Text.Encoding encoding = new System.Text.UnicodeEncoding();
                       FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(version,   // version
                                                 model.UserName,                                       // username
                                                 DateTime.Now,                                         // creation
                                                 expire,                                               // Expiration
                                                 flag,                                                 // Persistent
                                                 System.Convert.ToBase64String(s.token)                // encoding.GetString(Sesja) // Sesja
                                               );

                       // Encrypt the ticket.
                       string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                       // Create a cookie and add the encrypted ticket to the cookie
                       HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName,encryptedTicket);
                       Response.Cookies.Add(authCookie);
                       Session["NCLMenu"] = null;
                       
                       NCLMembershipProvider provider = (NCLMembershipProvider)Membership.Provider;
                       if (s.czyPierwszy == 1) //provider._repository.GetPwdChange(Sesja) == true)
                       {
                           return RedirectToAction("Manage");
                       }
                       else if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 0 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                       {
                           return Redirect(returnUrl);
                       }
                       else
                       {
                           return RedirectToAction("Index", "Home", null);
                       }

                    //}//flag
               }
               else
               {
                   ModelState.Clear(); //Czyszczę błędy - bo procedura zwraca co jest nieprawidłowe
                   ModelState.AddModelError("", "Zła nazwa użytkownika lub hasło");//"Brak ważnej sesji lub błąd logowania");
               }//ValidateLogOn
            }
            else
            {
                   ModelState.AddModelError("", "Zła nazwa użytkownika lub hasło");
            }//ModelStateValid

            //return RedirectToAction("Index", "Home");
            ViewBag.ReturnUrl = returnUrl;
            return View(model);

        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string token = "")
        {
            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "ResetPassword";

            ResetPasswordModel model = new ResetPasswordModel();
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    rez.status = -1;
                    rez.message = "Jesteś zalogowany. Użyj opcji zmiany hasła lub wyloguj się";
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    return LogOff();
                }

                model.KodResetujacy = token;
                model.Password = "";
            }
            catch (Exception ex)
            {
                rez.message = ex.Message;
                rez.status = -1;
            }


            if (rez.status == 0)
            {

            }
            else
            {
                TempData[rez.statusString] = string.Format("Resetowanie hasła wywołało błąd: {1} {0}", Environment.NewLine, rez.message);
                
            }

            return View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Błąd nieznany";

            NCLMembershipProvider provider = (NCLMembershipProvider)Membership.Provider;

            try
            {
                if (ModelState.IsValid)
                {

                    rez = provider.ResetPasswordByToken(model.KodResetujacy, model.Password, model.ConfirmPassword);
                 }
                else
                {
                    rez.status = -1;
                    rez.message = "Nie uzupełniono wszystkich pól";
                }
            }
            catch (Exception ex)
            {
                rez.message = ex.Message;
                rez.status = -1;
            }


            if (rez.status == 0)
            {
                TempData[rez.statusString] = rez.message;
            }
            else
            {
                TempData[rez.statusString] = string.Format("Resetowanie hasła wywołało błąd: {1} {0}", Environment.NewLine, rez.message);
                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgottenPassword()
        {
            ForgottenPasswordModel model = new ForgottenPasswordModel();

            RezultatObject rez = new RezultatObject();
            rez.status = 0;
            rez.message = "Błąd nieznany";

            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    rez.status = -1;
                    rez.message = "Jesteś zalogowany. Użyj opcji zmiany hasła lub wyloguj się";
                    Session.Clear();
                    Session.Abandon();
                    Session.RemoveAll();
                    return LogOff();
                }

            }
            catch (Exception ex)
            {
                rez.message = ex.Message;
                rez.status = -1;
            }

            
            
            return View(model);

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgottenPassword(ForgottenPasswordModel model)
        {

            Session.Clear();
            Session.RemoveAll();

            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Brak odpowiedzi od serwera";

            NCLMembershipProvider provider = (NCLMembershipProvider)Membership.Provider;

            try
            {
                if (ModelState.IsValid)
                {

                    //Wyslanie do bazy nazwy usera
                    //Wygenerowanie w bazie tokena do resetu
                    //Wyslanie e-maila
                    rez = provider.ForgottenPassowrdSendToken(model.UserName);
                   
                }
            }
            catch (Exception ex)
            {
                rez.message = ex.Message;
                rez.status = -1;
            }


            if (rez.status == 0)
            {
                TempData[rez.statusString] = rez.message;
            }
            else
            {
                TempData[rez.statusString] = string.Format("Resetowanie hasła wywołało błąd: {1} {0}", Environment.NewLine, rez.message);
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
           
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();
            Session["NCLMenu"] = null;

 
            return RedirectToAction("Index", "Home", null);

        }

        private SecurityToken ValidateLogOn(string userName, string password)
        {
            NCLMembershipProvider provider = (NCLMembershipProvider)Membership.Provider;
            SecurityToken s = null;

            if (String.IsNullOrEmpty(userName))
            {
                ModelState.AddModelError("username", "Podaj nazwę użytkownika.");
            }
            if (String.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("password", "Podaj hasło.");
            }
   
            if (ModelState.IsValid)
            {   
                //logowanie....
                s = provider.GetSesjaToken(userName, password);
            }
            return s;
        }

        //
        // GET: /Account/Manage
        [Authorize]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.Message =
                message == ManageMessageId.ChangePasswordSuccess ? "Twoje hasło zostało zmienione."
                : message == ManageMessageId.SetPasswordSuccess ? "Twoje hasło zostało ustawione."
                : " ";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {   
            ViewBag.ReturnUrl = Url.Action("Manage");
            ViewBag.Message = " ";

            if (ModelState.IsValid)
            {
                    NCLMembershipProvider provider = (NCLMembershipProvider)Membership.Provider;

                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = provider.ChangePassword(((CustomPrincipal)User).Identity.Sesja, model.OldPassword, model.NewPassword);

                    }
                    catch (SesjaException sx)
                    {
                        ModelState.AddModelError("", sx.Message);
                        TempData["msg"] = sx.Message;
                        return RedirectToAction("Login", "Account");
                    }
                    catch (Exception )
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Obecne hasło jest nieprawidłowe, lub nowe hasło nie spełnia wymagań dla hasła.");
                    }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Nazwa juz istnieje. Podaj inną nazwę.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Użytkownik z tym adesem e-mail już istnieje. Podaj inny adres e-mail.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Podane hasło jest nieważne. Podaj poprawne hasło.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Podany adres e-mail jest niepoprawny. Podaj poprawny adres e-mail.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Podana odpowiedź jest nieważna. Spróbuj jeszcze raz.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Podane ptyanie pomocnicze jest niepoprawne.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Błędna nazwa uzytkownika. Podaj poprawną nazwa i spróbuj jeszcze raz.";

                case MembershipCreateStatus.ProviderError:
                    return "Błąd providera autentykacji. Wpisz nazwę uzytkownika i hasło jeszcze raz. Jeśli nadal bedzie występował błąd, to skontaktuj sie z administratorem systemu NCL.";

                case MembershipCreateStatus.UserRejected:
                    return "Operacja utworzenia użytkownika została przerwana. Sprawdź wpisane dane i spróbuj jeszcze raz. Jeśli problem nadal bedzie występował, to skontaktuj sie z administratorem systemu NCL.";

                default:
                    return "Nieznany błąd. Sprawdź wpisane dane i spróbuj jeszcze raz. Jeśli problem nadal bedzie występował, to skontaktuj sie z administratorem systemu NCL.";
            }
        }
        #endregion

        #region "Error handle"

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is HttpAntiForgeryException)
            {

                this.RedirectToAction("Index", "Home", null);

                //this.RedirectToAction(
                //    filterContext.RouteData.Values["action"].ToString(),
                //    filterContext.RouteData.Values);
                filterContext.ExceptionHandled = true;
            }

            base.OnException(filterContext);
        }

#endregion
    }

}
