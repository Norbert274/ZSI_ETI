using System;
using System.Text;

namespace nclprospekt.Validation.ValidationErrors
{
    public enum DateOfBirthErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidDateFormat = 2,
        InvalidEntry = 3
    }

    public enum AmountErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidNumericFormat = 2,
        InvalidEntry = 3
    }

    public enum DetailNameErrors
    {
        Ok = 0,
        EntryRequired = 1
    }

    public enum NIPErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidLength = 2,
        InvalidCharacters = 3,
        InvalidEntry = 4
    }

    public enum ComboBoxSelectionErrors
    {
        Ok = 0,
        EntryRequired = 1
    }

    public enum PhoneNumberErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidNumericFormat = 2,
        InvalidEntry = 3
    }

    public enum NameErrors
    {
        Ok = 0,
        EntryRequired = 1
    }

    public enum EmailErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidEntry = 2
    }

    public enum WwwErrors
    {
        Ok = 0,
        EntryRequired = 1,
        InvalidEntry = 2
    }

    public struct ValidationErrorMessages
    {
        public static string GetMessage(ValidationRules rule, int errorCode)
        {
            switch (rule)
            {
                case ValidationRules.NIP:
                    switch (errorCode)
                    {
                        case (int)NIPErrors.InvalidEntry: return "Nieprawidłowa wartość numeru NIP";
                        case (int)NIPErrors.InvalidCharacters: return "Niedozwolone znaki w numerze NIP";
                        case (int)NIPErrors.InvalidLength: return "Nieprawidłowa długość numeru NIP";
                    }
                    break;
            }

            return string.Empty;
        }
    }
}