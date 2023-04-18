using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using nclprospekt.Exceptions;

namespace nclprospekt.Models
{

    public class UserRoleAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public UserRoleAuthorizeAttribute(){}
 
        public UserRoleAuthorizeAttribute(params UserRole[] roles)
        {
            Roles = string.Join(",", roles.Select(r => r.ToString()));
        }
    }
 
    public enum UserRole
    {
        Administrator = 1,
        User = 2
    }
    

    public static class SecurityExtentions
    {
        public static CustomPrincipal ToCustomPrincipal(this IPrincipal principal)
        {
            try
            {
            return (CustomPrincipal)principal;
            }
            
                catch (Exception ex)
            {
                string nic = ex.Message;
                   // Występuje czasem = błąd sesji
                //SesjaException sx = new SesjaException(ex.Message); 
                SesjaException sx = new SesjaException("Twoja sesja wygasła. Zaloguj się ponownie"); 
                throw sx;
            }            

        }
    }

    public class CustomPrincipal: IPrincipal
    {
        private readonly CustomIdentity _identity;

        public CustomPrincipal(CustomIdentity identity)
        {
            _identity = identity;
        }

        IIdentity IPrincipal.Identity
        {
            get { return _identity; }
        }

        public CustomIdentity Identity
        {
            get { return _identity; }
        }
        
        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
          !string.IsNullOrWhiteSpace(role) && Roles.IsUserInRole(Identity.Name, role); 
        }
    }

   

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
    }
    
    public class CustomMembershipUser : MembershipUser
    {
        #region Properties
 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; }
        public byte[] Sesja {get; set;}
 
        #endregion

        //TODO: uzyc user.SignUpDate, user.LastLoginDate, user.LastActiveTime zmaiast trzech pierwszych DateTime.Now,DateTime.Now, DateTime.Now,                                                  
        public CustomMembershipUser(Uzytkownik user)
            : base("NCLMembershipProvider", user.Nazwa, user.Uzytkownik_Id, user.Email, string.Empty, string.Empty, true, false,
            DateTime.Now,DateTime.Now, DateTime.Now, DateTime.Now,DateTime.Now)
        {
            FirstName = user.Imie;
            LastName = user.Nazwisko;
            if (user.Roles != null)
            {
                UserRoleId = (user.Roles.Count > 0)? user.Roles.FirstOrDefault().RoleId: 0;
                UserRoleName = (user.Roles.Count > 0)? user.Roles.FirstOrDefault().Name: null;
            }
        }
    }


    /// <summary>
    /// An identity object represents the user on whose behalf the code is running.
    /// </summary>
    [Serializable]
    public class CustomIdentity : IIdentity
    {
        #region Properties
 
        public IIdentity Identity { get; set; }
 
        public string FirstName { get; set; }
 
        public string LastName { get; set; }
 
        public string Email { get; set; }
 
        public int UserRoleId { get; set; }
 
        public string UserRoleName { get; set; }

        public byte[] Sesja { get; set; }
 
        #endregion
 
        #region Implementation of IIdentity
 
        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns>
        /// The name of the user on whose behalf the code is running.
        /// </returns>
        public string Name
        {
            get { return Identity.Name; }
        }
 
        /// <summary>
        /// Gets the type of authentication used.
        /// </summary>
        /// <returns>
        /// The type of authentication used to identify the user.
        /// </returns>
        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }
 
        /// <summary>
        /// Gets a value that indicates whether the user has been authenticated.
        /// </summary>
        /// <returns>
        /// true if the user was authenticated; otherwise, false.
        /// </returns>
        public bool IsAuthenticated { 
            get { 
                return Identity.IsAuthenticated; 
                //return !string.IsNullOrWhiteSpace(_name); 
            } 
        }
 
        #endregion
 
        #region Constructor
 
        public CustomIdentity(IIdentity identity)
        {
            Identity = identity;

            var customUser = (CustomMembershipUser)Membership.GetUser(identity.Name); //, false);
            if(customUser != null)
            {
                FirstName = customUser.FirstName;
                LastName = customUser.LastName;
                Email = customUser.Email;
                UserRoleId = customUser.UserRoleId;
                UserRoleName = customUser.UserRoleName;
                //Sesja = customUser.Sesja;
            }
        }
  
        #endregion
    }
}