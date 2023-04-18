using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;
using System.Web.Mvc;

namespace nclprospekt.Models
{
    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Obecne hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nowe hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć minimum {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        [Compare("NewPassword", ErrorMessage = "Nowe hasło i jego potwierdzenie nie są zgodne.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [RegularExpression(@"^[a-zA-Z0-9.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string Password { get; set; }
       
        [Display(Name = "Pamiętaj mnie?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [Display(Name = "Nazwa użytkownika")]
        [RegularExpression(@"^[a-zA-Z0-9.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć minimum {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie nie są zgodne.")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgottenPasswordModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        [Display(Name = "Nazwa użytkownika")]
        [RegularExpression(@"^[a-zA-Z0-9.ąćęłńóśźżĄĆĘŁŃÓŚŹŻ ]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        public string UserName { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Podanie kodu bezpieczeństwa jest konieczne")]
        [Display(Name = "Kod bezpieczeństwa")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,100}$", ErrorMessage = "Znaki specjalne są niedozwolone")]
        public string KodResetujacy { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [StringLength(100, ErrorMessage = "{0} powinno mieć minimum {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz hasło")]
        [Compare("Password", ErrorMessage = "Hasło i jego potwierdzenie nie są zgodne.")]
        [RegularExpression(@"^[a-zA-Z0-9!?ąćęłńóśźżĄĆĘŁŃÓŚŹŻ]{1,50}$", ErrorMessage = "Znaki specjalne są niedozwolone poza ! i ?")]
        public string ConfirmPassword { get; set; }
    }
}