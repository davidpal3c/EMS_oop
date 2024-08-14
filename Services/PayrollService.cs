using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using EMS.Models;
using EMS.Services;



namespace EMS.Services
{
    public class PayrollService
    {

        private readonly DBService _dbService;
        private readonly PayrollMapper _payrollMapper;


        /// <summary>
        /// Constructor for PayrollService class, instanting DBService and PayrollMapper objects to be used in the class.
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="payrollMapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PayrollService(DBService dbService, PayrollMapper payrollMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _payrollMapper = payrollMapper ?? throw new ArgumentNullException(nameof(payrollMapper));
        }



        /// <summary>
        /// Async method generating query to fetch employees from the database.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="sort">component provided sorting string</param>
        /// <returns>A task async returning list of Payroll objects mapped from the database using the sort string</returns>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GetPayrolls
        /// Date: 2024-08-10        
        /// </remarks>
        public async Task<List<Payroll>> GetPayrolls(string sort)
        {
            List<Payroll> payrollList = new List<Payroll>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT *  FROM Payrolls_view order by {sort}", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                Payroll payroll = _payrollMapper.MapFromReaderPayroll(reader);
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


        /// <summary>
        /// Async method generating query to fetch payrolls based user search parameter. 
        /// </summary>       
        /// <typeparam name="string"></typeparam>
        /// <param name="search"></param>
        /// <returns>Asynchronous task returning a list of Payroll objects from search query.>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: SearchPayrolls
        /// Date: 2024-08-10      
        /// </remarks>
        public async Task<List<Payroll>> SearchPayrolls(string search)
        {
            List<Payroll> payrollList = new List<Payroll>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM Payrolls_view where Name like '%{search}%'", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                Payroll payroll = _payrollMapper.MapFromReaderPayroll(reader);
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


        /// <summary>
        /// Async method generating insert of employee object in database
        /// </summary>       
        /// <typeparam name="object"></typeparam>
        /// <param name="List"></param>        
        /// <remarks>
        /// Name: AddEmployee
        /// Date: 2024-08-10 
        /// Parametizes insert query to add employee data into the database, using index of objects in list.
        /// </remarks>
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


        /// <summary>
        /// Asynchronous task updatig payroll field into database
        /// </summary>       
        /// <typeparam name="Payroll"></typeparam>
        /// <param name="updatedPayroll"></param>        
        /// <remarks>
        /// Name: UpdateEmployee
        /// Date: 2024-08-10  
        /// Method used to update employee fields in the database table 'Employees', based on employee id. 
        /// Parametizes update data in the database using index of objects in list.
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


        /// <summary>
        /// Asynchronous task deleting payroll field from database
        /// </summary>       
        /// <typeparam name="Payroll"></typeparam>
        /// <param name="payroll"></param>        
        /// <remarks>
        /// Name: DeleteEmployee
        /// Date: 2024-08-10 
        /// Method used to delete payroll fields in the database table 'Payroll', based on payroll id. 
        /// </remarks>
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
