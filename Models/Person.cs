using System.Security.Principal;
using LibraryManagementSystem.Enums;
namespace LibraryManagementSystem.Models

{
 public abstract class Person
    {
         protected int _id;
        protected string _name = string.Empty;
        protected string _email = string.Empty;
        protected string _phone = string.Empty;
        protected string _address = string.Empty;
        protected DateTime _dateOfBirth;
        protected DateTime _registrationDate;


    protected Person()
{
    RegistrationDate = DateTime.Now;
}

     public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : 0;
        }

        public string Name
        {
            get => _name;
            set => _name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public string Email
        {
            get => _email;
            set => _email = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public string Address
        {
            get => _address;
            set => _address = string.IsNullOrWhiteSpace(value) ? "N/A" : value;
        }

        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value < DateTime.Now)
                    _dateOfBirth = value;
            }
        }

        public DateTime RegistrationDate
        {
            get => _registrationDate;
            protected set => _registrationDate = value;
        }



        public virtual int CalculateAge()
        {
            int age = DateTime.Now.Year - DateOfBirth.Year;

            if (DateTime.Now < DateOfBirth.AddYears(age))
                age--;

            return age;
        }
         public abstract void DisplayInfo();
        
        public virtual void UpdateContactInfo(string email, string phone, string address)
        {
            Email = email;
            Phone = phone;
            Address = address;
        }

}
}