using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace WebApiUser.Validators
{
    public static class UserValidator
    {
        public static string Validate(string name, string email, string password)
        {
            string errors = string.Empty;

            errors += ValidateName(name);
            errors += ValidateEmail(email);
            errors += ValidatePassword(password);

            return errors;
        }

        public static string ValidateName(string name)
        {
            string message = string.Empty;
            if (name.Length < 2 || name.Length > 60 || name.IsNullOrEmpty())
                message = "O nome deve conter entre 2 e 60 caracteres. ";

            return message;
        }
        public static string ValidateEmail(string email)
        {
            string message = string.Empty;
            var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");

            if (!regex.IsMatch(email))
                message = "Insira um endereço de email válido. ";
            return message;
        }

        public static string ValidatePassword(string password)
        {
            string message = string.Empty;
            var regex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!regex.IsMatch(password))
                message = "A senha deve conter no mínimo 8 caracteres, sendo 1 maiúsculo, 1 minúsculo, 1 número e 1 caracter especial. ";

            return message;
        }
    }
}
