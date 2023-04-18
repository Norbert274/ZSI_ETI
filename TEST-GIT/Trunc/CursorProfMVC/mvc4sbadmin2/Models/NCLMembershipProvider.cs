using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Web.Caching;
using System.Web.ApplicationServices; 
using System.Web.Configuration;
using System.Configuration;
using nclprospekt.Models;
using nclprospekt.Repository;
using Ninject;
using Ninject.Modules;
using nclprospekt.Objects;
//using System.Configuration.Provider;
//using System.Security.Cryptography;
//using System.Text;

namespace nclprospekt.Models
{
    public class NCLMembershipProvider : MembershipProvider
    {
        #region Initial Values

        private string _applicationName = "NCL";
        private bool _enablePasswordReset = true;
        private bool _enablePasswordRetrieval = false;
        private bool _requiresQuestionAndAnswer = false;
        private bool _requiresUniqueEmail = true;
        private int _maxInvalidPasswordAttempts = 5;
        private int _passwordAttemptWindow = 10;
        private int _minRequiredPasswordLength = 8;

        //private MembershipPasswordFormat passwordFormat;
        //private int _minRequiredNonAlphanumericCharacters;
        //private string _passwordStrengthRegularExpression;
        //private MachineKeySection machineKey; //Used when determining encryption key values

        #endregion

        #region Properties

        //[Inject]
        public IUzytkownikRepository _repository; //{private get; set;}

        private int _cacheTimeoutInMinutes = 3600;
        private int czyPierwszy { get; set; }

        #endregion

        public NCLMembershipProvider()
            : base()
        {

        }

        public NCLMembershipProvider(IUzytkownikRepository repository)
            : base()
        {
            _repository = repository; //?? UserRepositoryFactory.GetRepository();
        }

        #region Overrides of MembershipProvider

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

            if (config == null)
            {
                string configPath = "~/web.config";
                Configuration NexConfig = WebConfigurationManager.OpenWebConfiguration(configPath);
                MembershipSection section = (MembershipSection)NexConfig.GetSection("system.web/membership");
                ProviderSettingsCollection settings = section.Providers;
                NameValueCollection membershipParams = settings[section.DefaultProvider].Parameters;
                config = membershipParams;
            }

            if (name == null || name.Length == 0)
            {
                name = "NCLMembershipProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "NCL Membership Provider");
            }

            // Call base method
            base.Initialize(name, config);

            //_applicationName = GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //_maxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            //_passwordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            //_minRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredAlphaNumericCharacters"], "1"));
            //_minRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            //_passwordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], String.Empty));
            //_enablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            //_enablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            //_requiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            //_requiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));

            //string temp_format = config["passwordFormat"];
            //if (temp_format == null)
            //{
            //    temp_format = "Hashed";
            //}

            //switch (temp_format)
            //{
            //    case "Hashed":
            //        passwordFormat = MembershipPasswordFormat.Hashed;
            //        break;
            //    case "Encrypted":
            //        passwordFormat = MembershipPasswordFormat.Encrypted;
            //        break;
            //    case "Clear":
            //        passwordFormat = MembershipPasswordFormat.Clear;
            //        break;
            //    default:
            //        throw new ProviderException("Password format not supported.");
            //}

            //Get encryption and decryption key information from the configuration.
            //System.Configuration.Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //machineKey = cfg.GetSection("system.web/machineKey") as MachineKeySection;

            //if (machineKey.ValidationKey.Contains("AutoGenerate"))
            //{
            //    if (PasswordFormat != MembershipPasswordFormat.Clear)
            //    {
            //        throw new ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.");
            //    }
            //}


        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public bool ChangePassword(byte[] sesja, string oldPassword, string newPassword)
        {
            bool retVal = false;

            if ((sesja == null) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword)) return false;

            if (oldPassword == newPassword) return false;
            
            retVal = _repository.ChangePassword(sesja, oldPassword, newPassword);
            
            return retVal;
        }

        public RezultatObject ForgottenPassowrdSendToken(string username)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Nie podano nazwy użytkownika";

            if ((username == null) || string.IsNullOrWhiteSpace(username)) return rez;

            rez = _repository.ForgottenPassowrdSendToken(username);

            return rez;
        }
        public RezultatObject ResetPasswordByToken(string token, string newPassword, string repeatPassword)
        {
            RezultatObject rez = new RezultatObject();
            rez.status = -1;
            rez.message = "Bład w przesłanych danych";

            if ((token == null) || string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(repeatPassword)) return rez;

            if (repeatPassword != newPassword)
            {
                rez.status = -1;
                rez.message = "Podane hasła nie są zgodne";
            }

            rez = _repository.ResetPasswordByToken(token, newPassword);

            return rez;
        }



        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { return _enablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _enablePasswordRetrieval; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        //public CustomMembershipUser GetUser(string username)
        //{
        //    CustomMembershipUser customUser = null;

        //    var cacheKey = string.Format("UserData_{0}", username);
        //    if (HttpRuntime.Cache[cacheKey] != null)
        //        return (CustomMembershipUser)HttpRuntime.Cache[cacheKey];

        //    Uzytkownik user = _repository.GetByUserName(username);

        //    if (user != null)
        //    {
        //        customUser = new CustomMembershipUser(user);
        //            //this.Name,
        //            //user.Nazwa,
        //            //null,
        //            //user.Email,
        //            //"",
        //            //"",
        //            //true,
        //            //false,
        //            //DateTime.Now,
        //            //DateTime.Now,
        //            //DateTime.Now,
        //            //default(DateTime),
        //            //default(DateTime),
        //            //null,
        //            //user.Nazwa);
        //    }

        //    return customUser;
        //}

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var cacheKey = string.Format("UserData_{0}", username);

            if (HttpRuntime.Cache[cacheKey] != null)
                return (MembershipUser)HttpRuntime.Cache[cacheKey];

            Uzytkownik user = null; // _repository.GetByUserName(username);
            //using (var context = new MvcDemoEntities())
            // {
            //var user = (from u in context.Users.Include(usr => usr.UserRole)
            //        where String.Compare(u.Username, username, StringComparison.OrdinalIgnoreCase) == 0
            //              && !u.Deleted
            //        select u).FirstOrDefault();

            if (user == null)
                return null;

            var membershipUser = new CustomMembershipUser(user);

            //Store in cache
            HttpRuntime.Cache.Insert(cacheKey, membershipUser, null, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes), Cache.NoSlidingExpiration);

            return membershipUser;

        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _maxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _passwordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _requiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _requiresUniqueEmail; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            //Users.Exists(m = > m.Username = username && m.Password == password)

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            SecurityToken tokenSesji = GetSesjaToken(username, password);
            byte[] sesja = tokenSesji.token;
            //var hashedPassword = Crypto.HashPassword(password);
            //TODO: zmiana na bardziej bezpieczną forme, dodac zabezpieczenie przed DOS
            var hashedPassword = _repository.GetByUserName(sesja,username).Haslo;
            var doesPasswordMatch = Crypto.VerifyHashedPassword(hashedPassword, password);
            return doesPasswordMatch;


            //using (var context = new MvcDemoEntities())
            //{
            //    var user = (from u in <span class="skimlinks-unlinked">context.Users</span>
            //                where String.Compare(u.Username, username, StringComparison.OrdinalIgnoreCase) == 0
            //                      && String.Compare(u.Password, password, StringComparison.OrdinalIgnoreCase) == 0
            //                      && !u.Deleted
            //                select u).FirstOrDefault();

            //    return user != null;
            //}
            // return username != null;
        }

        public SecurityToken GetSesjaToken(string username, string password)//logowanie
        {
            //var hashedPassword = Crypto.HashPassword(password);
            //return _repository.GetNewSecurityToken(username, hashedPassword, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes));
            return _repository.GetNewSecurityToken(username, password, DateTime.Now.AddMinutes(_cacheTimeoutInMinutes));
        }

        #endregion

        #region "Funkcje pomocnicze"

        /// <summary>
        /// Get config value.
        /// </summary>
        /// <param name="configValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }

        /// <summary>
        /// Check the password format based upon the MembershipPasswordFormat.
        /// </summary>
        /// <param name="password">Password</param>
        /// <param name="dbpassword"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        //private bool CheckPassword(string password, string dbpassword)
        //{
        //    string pass1 = password;
        //    string pass2 = dbpassword;

        //    switch (PasswordFormat)
        //    {
        //        case MembershipPasswordFormat.Encrypted:
        //            pass2 = UnEncodePassword(dbpassword);
        //            break;
        //        case MembershipPasswordFormat.Hashed:
        //            pass1 = EncodePassword(password);
        //            break;
        //        default:
        //            break;
        //    }

        //    if (pass1 == pass2)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        /// <summary>
        /// UnEncode password.
        /// </summary>
        /// <param name="encodedPassword">Password.</param>
        /// <returns>Unencoded password.</returns>
        //private string UnEncodePassword(string encodedPassword)
        //{
        //    string password = encodedPassword;

        //    switch (PasswordFormat)
        //    {
        //        case MembershipPasswordFormat.Clear:
        //            break;
        //        case MembershipPasswordFormat.Encrypted:
        //            password =
        //              Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
        //            break;
        //        case MembershipPasswordFormat.Hashed:
        //            //HMACSHA1 hash = new HMACSHA1();
        //            //hash.Key = HexToByte(machineKey.ValidationKey);
        //            //password = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));

        //            throw new ProviderException("Not implemented password format (HMACSHA1).");
        //        default:
        //            throw new ProviderException("Unsupported password format.");
        //    }

        //    return password;
        //}

        /// <summary>
        /// Encode password.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Encoded password.</returns>
        //private string EncodePassword(string password)
        //{
        //    string encodedPassword = password;

        //    switch (PasswordFormat)
        //    {
        //        case MembershipPasswordFormat.Clear:
        //            break;
        //        case MembershipPasswordFormat.Encrypted:
        //            byte[] encryptedPass = EncryptPassword(Encoding.Unicode.GetBytes(password));
        //            encodedPassword = Convert.ToBase64String(encryptedPass);
        //            break;
        //        case MembershipPasswordFormat.Hashed:
        //            HMACSHA1 hash = new HMACSHA1();
        //            hash.Key = HexToByte(machineKey.ValidationKey);
        //            encodedPassword =
        //              Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
        //            break;
        //        default:
        //            throw new ProviderException("Unsupported password format.");
        //    }

        //    return encodedPassword;
        //}

        /// <summary>
        /// Converts a hexadecimal string to a byte array. Used to convert encryption key values from the configuration
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        #endregion

    }
}