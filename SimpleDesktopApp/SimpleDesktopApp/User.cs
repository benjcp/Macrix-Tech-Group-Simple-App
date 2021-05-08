using System;

namespace SimpleDesktopApp
{
    public class User
    {
        private string first_name, last_name, street_name, town, postal_code, phone_number;
        private DateTime date_of_birth;
        private int? apartment_number;

        public string FirstName
        {
            get { return first_name; }
            set { first_name = value; }
        }

        public string LastName
        {
            get { return last_name; }
            set { last_name = value; }
        }

        public string StreetName
        {
            get { return street_name; }
            set { street_name = value; }
        }

        public string Town
        {
            get { return town; }
            set { town = value; }
        }

        public string PostalCode
        {
            get { return postal_code; }
            set { postal_code = value; }
        }

        public int? ApartmentNumber
        {
            get { return apartment_number; }
            set { apartment_number = value; }
        }

        public string PhoneNumber
        {
            get { return phone_number; }
            set { phone_number = value; }
        }

        public DateTime DateOfBirth
        {
            get { return date_of_birth; }
            set { date_of_birth = value; }
        }

        public int Age
        {
            get { return (DateTime.Today - date_of_birth).Days / 365; }
        }
    }
}
