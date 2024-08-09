using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;
using MySqlConnector;



namespace EMS.Services
{
    public class EmployeeService 
    {
        private readonly DBService _dbService;
        private readonly EmployeeMapper _employeeMapper;
        
        public EmployeeService(DBService dbService, EmployeeMapper employeeMapper) 
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _employeeMapper = employeeMapper ?? throw new ArgumentNullException(nameof(employeeMapper));
        }   
                             

        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Employees", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {                           

                            while (await reader.ReadAsync())
                            {
                                Employee employee = _employeeMapper.MapFromReaderEmployees(reader);
                                employeeList.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception ($"Error fetching employees: {ex.Message}");            
            }

            return employeeList;
        }


    }

}
