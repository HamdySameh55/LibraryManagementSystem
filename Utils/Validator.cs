using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Utils
{
    public static class Validator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            return Regex.IsMatch(phone, @"^\d{10,15}$");
        }

        public static bool IsValidISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
                return false;

           
            isbn = isbn.Replace("-", "");

        
            return isbn.Length == 10 || isbn.Length == 13;
        }
    }
}
