using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAcademicApp.Services
{
    internal class DataValidation
    {
        public static bool CheckForNull(string s)
        {
            // Check if the provided entry is null. If it is, we return the check as false (i.e. failed)
            // Otherwise we return true (i.e. passed)
            if (String.IsNullOrEmpty(s)) { return false; }
            else { return true; }
        }

        public static bool CheckEmail(string email)
        {
            
            if (String.IsNullOrEmpty (email)) { return false; } // If the email is empty, the check fails

            var trimmedEmail = email.Trim();                    // Trim whitespace 
            if (trimmedEmail.EndsWith(".")) { return false; }   // If the email ends in "." it fails the test

            try
            {
                // Shortcutting by using the MailAddress class to verify the email address format.
                // We then return bool whether the trimmed email equals the verified email.
                // If it does, then the address is valid. If not, then it's invalid.
                var addr = new System.Net.Mail.MailAddress(email);  
                return addr.Address == trimmedEmail;
            }
            catch
            {
                // If the above check fails (i.e. the email is unvalidated by the MailAddress class),
                // then it's invalid and we return false.
                return false;
            }
            
        }

        public static bool CheckDates(DateTime first, DateTime last)
        {
            // If the first date is before the last date, return true
            // Otherwise return false
            return first < last ? true : false;
        }
    }
}