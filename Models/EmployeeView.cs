using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EMS.Models
{
    /// <summary>
    /// Handles employee data for the employee view
    /// </summary>
    public class EmployeeView : Employee
    {
      
        private int _count;
        private string? _title;

        

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }


        public EmployeeView() { }


        public EmployeeView(int count, string title)
        {
            _count = count;
            _title = title;
        }


        public EmployeeView(int id, string name, string email, string position, decimal salary, string status, int role, DateTime createdAt, DateTime updatedAt) 
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
