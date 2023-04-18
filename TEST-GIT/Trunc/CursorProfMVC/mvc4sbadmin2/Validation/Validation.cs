using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using nclprospekt.Objects;

namespace nclprospekt.Validation
{
    public static class Validation
    {
        public static int Validate(ValidationRules rule, bool required, params object[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (required && Convert.ToString(args[0], System.Globalization.CultureInfo.CurrentCulture).Length == 0)
                {
                    return 1;
                }
                if (!required && Convert.ToString(args[0], System.Globalization.CultureInfo.CurrentCulture).Length == 0)
                {
                    return 0;
                }
            }
            switch (rule)
            {
                case ValidationRules.DateOfBirth:
                    return (int)ValidateDateOfBirth(required, args);
                case ValidationRules.Amount:
                    return (int)ValidateAmount(required, args);
                case ValidationRules.NIP:
                    return (int)ValidateNIP(required, args);
                case ValidationRules.ComboBoxSelection:
                    return (int)ValidateComboBoxSelection(required, args);
                case ValidationRules.DetailName:
                    return (int)ValidateDetailName(required, args);
                case ValidationRules.Name:
                    return (int)ValidateName(required, args);
                case ValidationRules.PhoneNumber:
                    return (int)ValidatePhoneNumber(required, args);
                case ValidationRules.Email:
                    return (int)ValidateEmail(required, args);
                case ValidationRules.Www:
                    return (int)ValidateWww(required, args);
            }

            return 0;
        }

        public static string GetRuleErrorMessage(ValidationRules rule, int errorCode)
        {
            //TODO: Brak implementacji
            return string.Format(System.Globalization.CultureInfo.CurrentCulture, "Error description for rule: {0}, error code: {1}", rule, errorCode);
        }

        private static ValidationErrors.DateOfBirthErrors ValidateDateOfBirth(bool required, params object[] args)
        {

            if (args != null && args.Length > 0)
            {
                DateTime dob = DateTime.MinValue;
                if (!DateTime.TryParse(Convert.ToString(args[0], System.Globalization.CultureInfo.CurrentCulture), out dob))
                {
                    return ValidationErrors.DateOfBirthErrors.InvalidDateFormat;
                }
                if (dob >= DateTime.Now)
                {
                    return ValidationErrors.DateOfBirthErrors.InvalidEntry;
                }
            }

            return ValidationErrors.DateOfBirthErrors.Ok;
        }

        private static ValidationErrors.AmountErrors ValidateAmount(bool required, params object[] args)
        {

            if (args != null && args.Length > 0)
            {
                Decimal amount;

                if (!Decimal.TryParse(Convert.ToString(args[0], System.Globalization.CultureInfo.CurrentCulture), out amount))
                {
                    return ValidationErrors.AmountErrors.InvalidNumericFormat;
                }
                if (amount < 0)
                {
                    return ValidationErrors.AmountErrors.InvalidEntry;
                }
            }

            return ValidationErrors.AmountErrors.Ok;
        }

        private static ValidationErrors.NIPErrors ValidateNIP(bool required, params object[] args)
        {
            string value = args[0].ToString();

            if (value.Length != 10)
            {
                return ValidationErrors.NIPErrors.InvalidLength;
            }
            UInt64 tmp = 0;
            if (!UInt64.TryParse(value, out tmp))
            {
                return ValidationErrors.NIPErrors.InvalidCharacters;
            }

            int[] weights = new int[] { 6, 5, 7, 2, 3, 4, 5, 6, 7 };
            int i = 0;
            int sum = 0;
            foreach (int w in weights)
            {
                sum += (w * Convert.ToInt32(value[i++].ToString()));
            }

            if ((sum % 11) != Convert.ToInt32(value[i++].ToString()))
            {
                return ValidationErrors.NIPErrors.InvalidEntry;
            }


            return ValidationErrors.NIPErrors.Ok;
        }

        private static ValidationErrors.ComboBoxSelectionErrors ValidateComboBoxSelection(bool required, params object[] args)
        {

            if (args != null && args.Length > 0)
            {
                if (!(args[0] is ObjectItem) || (args[0] as ObjectItem).ID == -1)
                {
                    return ValidationErrors.ComboBoxSelectionErrors.EntryRequired;
                }
            }

            return ValidationErrors.ComboBoxSelectionErrors.Ok;
        }

        private static ValidationErrors.NameErrors ValidateName(bool required, params object[] args)
        {
            if (required == true)
            {
                // FIXME: Poprawić warunki logiczne
                if (args == null || args.Length == 0 || args[0].ToString().Length == 0)
                {
                    return ValidationErrors.NameErrors.EntryRequired;
                }
            }

            return ValidationErrors.NameErrors.Ok;
        }

        private static ValidationErrors.DetailNameErrors ValidateDetailName(bool required, params object[] args)
        {
            if (required == true)
            {
                // FIXME: Poprawić warunki logiczne
                if (args == null || args.Length == 0 || args[0].ToString().Length == 0)
                {
                    return ValidationErrors.DetailNameErrors.EntryRequired;
                }
            }

            return ValidationErrors.DetailNameErrors.Ok;
        }

        private static ValidationErrors.PhoneNumberErrors ValidatePhoneNumber(bool required, params object[] args)
        {
            if (required == true)
            {
                // FIXME: Poprawić warunki logiczne
                if (args == null || args.Length == 0 || args[0].ToString().Length == 0)
                {
                    return ValidationErrors.PhoneNumberErrors.EntryRequired;
                }
            }

            return ValidationErrors.PhoneNumberErrors.Ok;
        }

        private static ValidationErrors.EmailErrors ValidateEmail(bool required, params object[] args)
        {
            Regex re = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (args != null && args.Length > 0 && re.IsMatch(args[0].ToString()))
            {
                return ValidationErrors.EmailErrors.InvalidEntry;
            }

            return ValidationErrors.EmailErrors.Ok;
        }

        private static ValidationErrors.WwwErrors ValidateWww(bool required, params object[] args)
        {
            if (!args[0].ToString().StartsWith("http://"))
            {
                return ValidationErrors.WwwErrors.InvalidEntry;
            }

            return ValidationErrors.WwwErrors.Ok;
        }
    }
}