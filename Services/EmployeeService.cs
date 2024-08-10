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

                    using (MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Employees", conn))
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





        // TEMPORARY LOCATION FOR TESTING. TO MOVE TO PAYROLL SERVICE

        public async Task<List<Payroll>> GetPayrolls()
        {
            List<Payroll> payrollList = new List<Payroll>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Payrolls", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                Payroll payroll= _employeeMapper.MapFromReaderPayroll(reader);
                                payrollList.Add(payroll);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return payrollList;
        }

        public async Task<List<Payroll>> SearchPayrolls(string search)
        {
            List<Payroll> payrollList = new List<Payroll>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Payrolls where EmployeeId like '{search}'", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                Payroll payroll = _employeeMapper.MapFromReaderPayroll(reader);
                                payrollList.Add(payroll);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return payrollList;
        }


        public async Task AddPayroll(List<object> payrollData)
        {

            if (payrollData == null || !(payrollData.Count == 5))
                throw new ArgumentException("Invalid payroll data");

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = $"INSERT INTO Payrolls ( EmployeeId, BaseSalary, OvertimePay, Deductions, NetPay) VALUES ( @EmployeeId, @BaseSalary, @OvertimePay, @Deductions, @NetSalary);";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", payrollData[0]);
                    cmd.Parameters.AddWithValue("@BaseSalary", payrollData[1]);
                    cmd.Parameters.AddWithValue("@OvertimePay", payrollData[2]);
                    cmd.Parameters.AddWithValue("@Deductions", payrollData[3]);
                    cmd.Parameters.AddWithValue("@NetSalary", payrollData[4]);

                    //string cmdString = BuildQueryString(cmd);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task UpdatePayroll(Payroll updatedPayroll)
        {
            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = "UPDATE Payrolls SET EmployeeId = @EmployeeId, BaseSalary = @BaseSalary, OvertimePay = @OvertimePay, Deductions = @Deductions, NetPay = @NetSalary WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", updatedPayroll.EmployeeId);
                    cmd.Parameters.AddWithValue("@BaseSalary", updatedPayroll.BaseSalary);
                    cmd.Parameters.AddWithValue("@OvertimePay", updatedPayroll.OvertimePay);
                    cmd.Parameters.AddWithValue("@Deductions", updatedPayroll.Deductions);
                    cmd.Parameters.AddWithValue("@NetSalary", updatedPayroll.NetSalary);
                    cmd.Parameters.AddWithValue("@Id", updatedPayroll.Id);


                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task DeletePayroll(Payroll payroll)
        {

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = "DELETE FROM Payrolls WHERE Id = @Id";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", payroll.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }


    }

}
