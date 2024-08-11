﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EMS.Models
{
    public class TimeRecord : BaseEntity
    {

        private int _employeeId;
        private string _name;
        private DateTime _date;
        private double _hoursWorked;
        private string _type;


        [Required(ErrorMessage = "Employee ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Employee ID must be a positive integer")]
        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        public string EmployeeName
        {
            get { return _name; }
            set { _name = value; }
        }


        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Date must be a valid date")]
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }


        [Required(ErrorMessage = "Hours Worked is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Hours Worked must be a non-negative number")]
        public double HoursWorked
        {
            get { return _hoursWorked; }
            set { _hoursWorked = value; }
        }

        //enum selection
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }   

        public TimeRecord() { }


        public TimeRecord(int id, int employeeId, string name, DateTime date, double hoursWorked, string type, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            EmployeeId = employeeId;
            EmployeeName = name;
            Date = date;
            HoursWorked = hoursWorked;
            Type = type;
        }   


        public override string ToString()
        {
            return "";
        }

    }
}
