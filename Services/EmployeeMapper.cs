using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;   
using MySqlConnector;


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
    }
}
