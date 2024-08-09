using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Models
{
    public class Payroll : BaseEntity
    {
        private int _employeeId;
        private double _baseSalary;
        private double _overtimePay;
        private double _deductions;
        private double _netSalary;

    
        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        [Required(ErrorMessage = "Base Salary is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Base Salary must be a non-negative number")]
        [DataType(DataType.Currency, ErrorMessage = "Base Salary must be a valid currency amount")]
        public double BaseSalary
        {
            get { return _baseSalary; }
            set { _baseSalary = value; }
        }


        [Range(0, double.MaxValue, ErrorMessage = "Overtime Pay must be a non-negative number")]
        [DataType(DataType.Currency, ErrorMessage = "Overtime Pay must be a valid currency amount")]
        public double OvertimePay
        {
            get { return _overtimePay; }
            set { _overtimePay = value; }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Deductions must be a non-negative number")]
        [DataType(DataType.Currency, ErrorMessage = "Deductions must be a valid currency amount")]
        public double Deductions
        {
            get { return _deductions; }
            set { _deductions = value; }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Net Salary must be a non-negative number")]
        [DataType(DataType.Currency, ErrorMessage = "Net Salary must be a valid currency amount")]
        public double NetSalary
        {
            get { return _netSalary; }
            set { _netSalary = value; }
        }   


        public Payroll(int id, int employeeId, double baseSalary, double overtimePay, double deductions, double netSalary, DateTime createdAt, DateTime updatedAt) : base(id, createdAt, updatedAt)
        {
            EmployeeId = employeeId;
            BaseSalary = baseSalary;
            OvertimePay = overtimePay;
            Deductions = deductions;
            NetSalary = netSalary;
        }

        public override string ToString()
        {
            return "";
        }

    }
}
