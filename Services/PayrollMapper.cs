using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;


namespace EMS.Services
{
    public class PayrollMapper
    {

        /// <summary>
        /// Mapper method from reader, instantiating Payroll object.
        /// </summary>       
        /// <typeparam name="MySqlDataReader"></typeparam>
        /// <param name="reader"></param>        
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException">throws error mapping message related to invalid types</exception>
        /// <remarks>
        /// Name: MapFromReaderPayroll
        /// Date: 2024-08-10       
        /// </remarks>
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



        /// <summary>
        /// method moved to TimeRecordMapper.cs//
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
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
