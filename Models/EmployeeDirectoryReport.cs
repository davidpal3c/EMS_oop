using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
    /// <summary>
    /// Generates employee data
    /// </summary>
    public class EmployeeDirectoryReport : Report
    {
        private int _id;
        private string _name;
        private string _email;
        private string _position;
        private int _salary;
        private DateTime _createdAt;
        private DateTime _updatedAt;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Position { get { return _position; } set { _position = value; } }
        public int Salary { get { return _salary; } set { _salary = value; } }
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }
        public DateTime UpdatedAt { get { return _updatedAt; } set { _updatedAt = value; } }


        public EmployeeDirectoryReport()
        {
            ReportName = "Employee Directory Report";
            ReportQuery = EReportType.EmployeeDirectory;
        }
               

    }
}
