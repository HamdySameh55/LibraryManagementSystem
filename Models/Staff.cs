using System;
using LibraryManagementSystem.Enums;
namespace LibraryManagementSystem.Models
{
    
    public class Staff : Person
    {
       
        private UserRole _role;
        private decimal _salary;
        private DateTime _hireDate;
        private string _department = string.Empty;

      
        public UserRole Role
        {
            get => _role;
            set => _role = value;
        }

        public decimal Salary
        {
            get => _salary;
            set => _salary = value >= 0 ? value : 0;
        }

        public DateTime HireDate
        {
            get => _hireDate;
            set
            {
                if (value <= DateTime.Now)
                    _hireDate = value;
            }
        }

        public string Department
        {
            get => _department;
            set => _department = string.IsNullOrWhiteSpace(value) ? "General" : value;
        }


        public Staff()
        {
            HireDate = DateTime.Now;
            Salary = 0;
            Role = UserRole.ViewBooks;
            Department = "General";
        }

        public bool HasPermission(UserRole permission)
        {
            return (Role & permission) == permission;
        }

     
        public void PerformAction(string action, UserRole requiredPermission)
        {
            if (HasPermission(requiredPermission))
            {
                Console.WriteLine($"{Name} performed action: {action}");
            }
            else
            {
                Console.WriteLine($"{Name} does NOT have permission to perform: {action}");
            }
        }

      
        public override void DisplayInfo()
        {
            Console.WriteLine($"Staff: {Name}");
            Console.WriteLine($"Email: {Email}, Phone: {Phone}");
            Console.WriteLine($"Department: {Department}, HireDate: {HireDate.ToShortDateString()}, Salary: {Salary:C}");
            Console.WriteLine($"Permissions: {Role}");
        }
    }
}
