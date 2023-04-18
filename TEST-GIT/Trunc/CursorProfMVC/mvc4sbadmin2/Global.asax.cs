using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Web.Security;
using nclprospekt.Models;
using System.Web.WebPages;


namespace nclprospekt
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(nclprospekt.JDataTables.DataTablesRequest), new nclprospekt.JDataTables.DataTablesBinder());


            DisplayModes.RegisterDisplayModes(DisplayModeProvider.Instance);

            //Dodane w App_GlobalResources - przetlumaczone domyslne wiadomosci o bledach
            //Bo domyslnie w HTML helperach wpisywaly sie napisy po angielsku
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Messages";
            DefaultModelBinder.ResourceClassKey = "Messages";
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                //var identity = new CustomIdentity(HttpContext.Current.User.Identity);
                //var principal = new CustomPrincipal(identity);
                //HttpContext.Current.User = principal;

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    // Get the forms authentication ticket.
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                }

                // look if any security information exists for this request
                if (HttpContext.Current.User != null)
                {
                    // see if this user is authenticated, any authenticated cookie (ticket) exists for this user
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        // see if the authentication is done using FormsAuthentication
                        if (HttpContext.Current.User.Identity is FormsIdentity)
                        {
                            // Get the roles stored for this request from the ticket
                            // get the identity of the user
                            FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                            var customIdentity = new CustomIdentity(identity);
                            // Get the custom user data encrypted in the ticket.
                            //string userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;
                            // Get the form authentication ticket of the user
                            FormsAuthenticationTicket ticket = identity.Ticket;
                            //System.Text.Encoding encoding = new System.Text.UnicodeEncoding();
                            //customIdentity.Sesja = encoding.GetBytes(ticket.UserData);
                            customIdentity.Sesja = System.Convert.FromBase64String(ticket.UserData);
                            //Encoding.ASCII.GetBytes(ticket.UserData);
                            //Get the roles stored as UserData into ticket
                            //List<string> roles = new List<string>();
                            //if (identity.Name == "adminadmin")
                            //    roles.Add("Admin");
                            //Create general prrincipal and assign it to current request
                            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles.ToArray());
                            var principal = new CustomPrincipal(customIdentity);
                            HttpContext.Current.User = principal;
                        }
                    }
                }

                //HttpContext.Current.Cache.Insert(
                //uniqueCacheKey, userObject, null, DateTime.Now.AddMinutes(2),
                //Cache.NoSlidingExpiration);


            }
        }

        public static string GetDisplayModeId(System.Web.HttpContextBase context)
        {
            IList<IDisplayMode> modes = DisplayModeProvider.Instance.Modes;
            int length = modes.Count;

            for (var i = 0; i < length; i++)
            {
                if (modes[i].CanHandleContext(context))
                {
                    return modes[i].DisplayModeId;
                }
            }

            throw new Exception("No display mode could be found for the sent context.");
        }
     

    }
}