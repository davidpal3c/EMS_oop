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


        /// <summary>
        /// Async method generating query to fetch employees from the database.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="sort"></param>
        /// <returns>A task async returning list of Employee objects mapped from the database table 'Employees'</returns>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GetEmployees
        /// Date: 2024-08-10
        /// Uses mapping object to access the mapper method in charge of fetching database.
        /// </remarks>
        public async Task<List<Employee>> GetEmployees(string sort)
        {
            List<Employee> employeeList = new List<Employee>();
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees order by {sort}", conn))
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

        /// <summary>
        /// Async method generating query to fetch employee report view (1) from the database.
        /// </summary>        
        /// <param>No parameters</param>
        /// <returns>Asynchronous task returning a list of employees with more information from virtual table</returns>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GetEmployees
        /// Date: 2024-08-13
        /// Used to retrieve other fields from the database virtual table 'Employees_report1'.
        /// </remarks>
        public async Task<List<Employee>> GetEmployeeReport1()
        {
            List<EmployeeView> employeeList = new List<EmployeeView>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees_report1", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                EmployeeView employee = _employeeMapper.MapFromReaderEmployeeReport1(reader);
                                employeeList.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employees: {ex.Message}");
            }

            return employeeList;
        }

        /// <summary>
        /// Async method generating query to fetch employee report view (2) from the database.
        /// </summary>        
        /// <param>No parameters</param>
        /// <returns>Asynchronous task returning a list of employees with more information from virtual table
        /// <remarks>
        /// Name: GetEmployees
        /// Date: 2024-08-13
        /// Used to retrieve other fields from the database virtual table 'Employees_report2'.
        /// </remarks>
        public async Task<List<Employee>> GetEmployeeReport2()
        {
            List<EmployeeView> employeeList = new List<EmployeeView>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees_report2", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                EmployeeView employee = _employeeMapper.MapFromReaderEmployeeReport2(reader);
                                employeeList.Add(employee);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employees: {ex.Message}");
            }

            return employeeList;
        }



        public async Task<List<Employee>> SearchEmployees(string search)
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees where Name like '%{search}%' or Email like '{search}%' or Position like '%{search}%'", conn))
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
                throw new Exception($"Error fetching employees: {ex.Message}");
            }

            return employeeList;
        }


        public async Task<List<Employee>> FilterEmployees(string field, string search, string sort)
        {
            List<Employee> employeeList = new List<Employee>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Employees where {field} = '{search}' order by {sort}", conn))
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
                throw new Exception($"Error fetching employees: {ex.Message}");
            }

            return employeeList;
        }


        public async Task AddEmployee(List<object> employeeData)
        {

            if (employeeData == null || !(employeeData.Count == 6))
                throw new ArgumentException("Invalid customer data");

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = $"INSERT INTO Employees ( Name, Email, Position, Salary, EmployeeStatus, EmployeeRole) VALUES ( @Name, @Email, @Position, @Salary, @Status, @Role);";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", employeeData[0]);
                    cmd.Parameters.AddWithValue("@Email", employeeData[1]);
                    cmd.Parameters.AddWithValue("@Position", employeeData[2]);
                    cmd.Parameters.AddWithValue("@Salary", employeeData[3]);
                    cmd.Parameters.AddWithValue("@Status", employeeData[4]);
                    cmd.Parameters.AddWithValue("@Role", employeeData[5]);

                    //string cmdString = BuildQueryString(cmd);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task UpdateEmployee(Employee updatedEmployee)
        {
            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = "UPDATE Employees SET Name = @Name, Email = @Email, Position = @Position, Salary = @Salary, EmployeeStatus = @Status, EmployeeRole = @Role WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", updatedEmployee.Name);
                    cmd.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                    cmd.Parameters.AddWithValue("@Position", updatedEmployee.Position);
                    cmd.Parameters.AddWithValue("@Salary", updatedEmployee.Salary);
                    cmd.Parameters.AddWithValue("@Status", updatedEmployee.Status);
                    cmd.Parameters.AddWithValue("@Role", updatedEmployee.Role);
                    cmd.Parameters.AddWithValue("@Id", updatedEmployee.Id);
            

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeleteEmployee(Employee employee)
        {

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = "DELETE FROM Employees WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", employee.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }


        

    }

}
