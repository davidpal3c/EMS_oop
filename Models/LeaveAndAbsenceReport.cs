using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
    /// <summary>
    /// Generates a leave and absence report
    /// </summary>
    public class LeaveAndAbsenceReport : Report
    {
        private int _id;
        private string _name;
        private DateTime _date;
        private double _hoursWorked;
        private string _email;
        private string _position;
        private DateTime _createdAt;


        public int Id { get { return _id; } set { _id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public double HoursWorked { get { return _hoursWorked; } set { _hoursWorked = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string Position { get { return _position; } set { _position = value; } }        
        public DateTime CreatedAt { get { return _createdAt; } set { _createdAt = value; } }    


        public LeaveAndAbsenceReport()
        {
            ReportName = "Leave and Absence Report";
            ReportQuery = EReportType.LeaveAndAbsence;
        }   
    }
}
