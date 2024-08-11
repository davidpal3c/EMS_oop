using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;   
using MySqlConnector;
using System.Data;


namespace EMS.Services
{
    public class EmployeeMapper
    {

        public Employee MapFromReaderEmployees(MySqlDataReader reader)
        {
            try
            {
                Employee employee = new Employee();


                return new Employee
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    Position = reader.GetString("Position"),
                    Salary = reader.GetDecimal("Salary"),
                    Status = reader.GetString("EmployeeStatus"),
                    Role = reader.GetInt32("EmployeeRole"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.GetDateTime("UpdatedAt"),
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to Employee", ex);
            }

        }

        // TEMPORARY LOCATION - TO MOVE TO PAYROLL MAPPER

        public Payroll MapFromReaderPayroll(MySqlDataReader reader)
        {
            try
            {
                Payroll payroll = new Payroll();


                return new Payroll
                {
                    Id = reader.GetInt32("Id"),
                    EmployeeId = reader.GetInt32("EmployeeId"),
                    EmployeeName = reader.GetString("Name"),
                    BaseSalary = reader.GetDouble("BaseSalary"),
                    OvertimePay = reader.GetDouble("OvertimePay"),
                    Deductions = reader.GetDouble("Deductions"),
                    NetSalary = reader.GetDouble("NetPay"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.GetDateTime("UpdatedAt"),
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to Payroll", ex);
            }

        }



        public TimeRecord MapFromReaderTime(MySqlDataReader reader)
        {
            try
            {
                TimeRecord payroll = new TimeRecord();


                return new TimeRecord
                {
                    Id = reader.GetInt32("Id"),
                    EmployeeId = reader.GetInt32("EmployeeId"),
                    EmployeeName = reader.GetString("Name"),
                    Date = reader.GetDateTime("Date"),
                    HoursWorked = reader.GetDouble("HoursWorked"),
                    //TimeRecord.TimeType = reader.GetEnumerator("Type"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.GetDateTime("UpdatedAt")
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to TimeRecord", ex);
            }

        }
    }
}
