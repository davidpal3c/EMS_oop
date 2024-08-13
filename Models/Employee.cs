using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EMS.Models
{
    public class Employee : BaseEntity
    {
        private string _name;
        private string _email;
        private string _position;
        private decimal _salary;
        private string _status;
        private int _role;


        [Required(ErrorMessage = "Eemployee Name required")]
        [StringLength(50, ErrorMessage = "Employee Name cannot exceed 100 characters")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        [Required(ErrorMessage = "Email required")]
        [StringLength(50, ErrorMessage = "Employee Name cannot exceed 100 characters")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [Required(ErrorMessage = "Position required")]
        [StringLength(50, ErrorMessage = "Employee Name cannot exceed 100 characters")]
        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number")]
        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public int Role
        {
            get { return _role; }
            set { _role = value; }
        }


        public Employee() { }

        public Employee(int count, string status) 
        { 
            Id = count;
            Status = status;
        
        }

       

        public Employee(int id, string name, string email, string position, decimal salary, string status, int role, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Position = position;
            Salary = salary;
            Status = status;
            Role = role;
        }

        public override string ToString()
        {
            return $"Employee: {Name}, Email: {Email}, Position {Position}, Salary: {Salary}, CreatedAt: {CreatedAt}, UpdatedAt {UpdatedAt}";
        }

    }
}
