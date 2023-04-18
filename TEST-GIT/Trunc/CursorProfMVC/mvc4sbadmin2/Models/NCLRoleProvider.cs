using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using nclprospekt.Models;
using nclprospekt.Repository;

namespace nclprospekt.Models
{
    public class NCLRoleProvider : RoleProvider
    {
        #region Properties

        //[Inject]
        public IUzytkownikRepository _repository; //{private get; set;}

        private int _cacheTimeoutInMinutes = 30;

        #endregion

        public NCLRoleProvider()
            : base()
        {
            //_repository = repository; //?? UserRepositoryFactory.GetRepository();
        }

        public NCLRoleProvider(IUzytkownikRepository repository)
            : base()
        {
            _repository = repository;
        }

        #region Overrides of RoleProvider

        /// <summary>
        /// Initialize values from web.config.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        public override void Initialize(string name, NameValueCollection config)
        {
            // Set Properties
            int val;
            if (!string.IsNullOrEmpty(config["cacheTimeoutInMinutes"]) && Int32.TryParse(config["cacheTimeoutInMinutes"], out val))
                _cacheTimeoutInMinutes = val;

            // Set the _repository
            AccountRepositoryFactory factory = new AccountRepositoryFactory();
            //= new AccountRepositoryFactory();
            _repository = factory.GetRepository();

            // Call base method
            base.Initialize(name, config);
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of the roles that a specified user is in for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles that the specified user is in for the configured applicationName.
        /// </returns>
        /// <param name="username">The user to return a list of roles for.</param>
        public override string[] GetRolesForUser(string username = "Brak takiego usera")
        {

            //Return if the user is not authenticated
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                return null;

            //Return if present in Cache
            var cacheKey = string.Format("UserRoles_{0}", username);
            if (HttpRuntime.Cache[cacheKey] != null)
                return (string[])HttpRuntime.Cache[cacheKey];

            //Get the roles from DB
            IEnumerable<Role> roles = null;
            var userRoles = new string[] { };

            //dodane...
            CustomPrincipal principal = (CustomPrincipal)HttpContext.Current.User;
            //TODO: uzyc login zamiast sesji...
            //zmienione aby uzyc sesji...
            roles = _repository.GetRolesForUser(principal.Identity.Sesja);

            //using (var context = new MvcDemoEntities())
            //{
            //    var user = (from u in context.Users.Include(usr => usr.UserRole)
            //                where String.Compare(u.Username, username, StringComparison.OrdinalIgnoreCase) == 0
            //                select u).FirstOrDefault();

            if (roles != null)
            {
                //string[] 
                userRoles = new string[roles.Count()];
                int idx = 0;
                foreach (Role ur in roles)
                    userRoles[idx++] = ur.Name;
            }
            //        userRoles = new[]{user.UserRole.UserRoleName};
            //}

            //Store in cache
            HttpRuntime.Cache.Insert(cacheKey, userRoles, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

            // Return
            if (userRoles != null)
                return userRoles.ToArray();
            else
                return null;

        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for the configured applicationName.
        /// </summary>
        /// <returns>
        /// true if the specified user is in the specified role for the configured applicationName; otherwise, false.
        /// </returns>
        /// <param name="username">The user name to search for.</param><param name="roleName">The role to search in.</param>

        public override bool IsUserInRole(string username, string roleName)
        {
            var userRoles = GetRolesForUser(username);
            return userRoles.Contains(roleName);

            //User user = _repository.GetByUserName(username);
            //if (user != null)
            //    return user.IsInRole(roleName);
            //else
            //    return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}