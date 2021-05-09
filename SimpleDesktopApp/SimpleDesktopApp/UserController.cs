using System;
using System.Data;
using System.Text.RegularExpressions;

namespace SimpleDesktopApp
{
    public static class UserController
    {

        public const string EMPTY_ERROR_MESSAGE = @"Value must not be empty";
        public const string APARTMENT_NUMBER_ERROR_MESSAGE = @"Value must not be less than 1";
        public const string PHONE_NUMBER_ERROR_MESSAGE = @"Please enter a valid phone number.";
        public const string FUTURE_DATETIME_ERROR_MESSAGE = @"Please enter a date that is in the past.";
        public const string INVALID_DATETIME_ERROR_MESSAGE = @"Please enter a valid DateTime.";

        public static string ValidateField(string colName ,string value)
        {

            switch (colName)
            {
                case "FirstName":
                case "LastName":
                case "StreetName":
                case "Town":
                case "PostalCode":
                    if (string.IsNullOrEmpty(value))
                    {
                        return EMPTY_ERROR_MESSAGE;
                    }
                    break;

                case "ApartmentNumber":
                    if (!string.IsNullOrEmpty(value) && (!int.TryParse(value, out int result) || result < 1))
                    {
                        return APARTMENT_NUMBER_ERROR_MESSAGE;
                    }
                    break;

                case "PhoneNumber":
                    string pattern = @"^[0-9-+]{9,15}$";

                    if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, pattern))
                    {
                        return PHONE_NUMBER_ERROR_MESSAGE;
                    }
                    break;

                case "DateOfBirth":
                    if (DateTime.TryParse(value, out DateTime dateOfBirth))
                    {
                        if (dateOfBirth > DateTime.Today)
                        {
                            return FUTURE_DATETIME_ERROR_MESSAGE;
                        }
                    }
                    else
                    {
                        return INVALID_DATETIME_ERROR_MESSAGE;
                    }
                    break;
                default:
                    {
                        return null;
                    }
            }

            return null;
        }
    }
}
