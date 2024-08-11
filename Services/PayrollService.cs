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

        public PayrollService(DBService dbService, PayrollMapper payrollMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _payrollMapper = payrollMapper ?? throw new ArgumentNullException(nameof(payrollMapper));
        }


        public async Task<List<Payroll>> GetPayrolls()
        {
            List<Payroll> payrollList = new List<Payroll>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT *  FROM Payrolls_view", conn))
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
