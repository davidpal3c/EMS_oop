using MySqlConnector;
using System;
using EMS.Models;

namespace EMS.Services
{
    /// Date last updated: 2024-08-13
    public class DBService
    {
        /// <summary>
        /// DBService class is responsible for handling database operations in the system.
        /// Provides connection string (to the database) and mapper objects for the system.
        /// </summary>
        /// Date: 2024-08-11
        private readonly string _connectionString;
        private readonly EmployeeMapper _employeeMapper;
        private readonly PayrollMapper _payrollMapper;
        private readonly TimeRecordMapper _timeRecordMapper;
        private readonly ReportMapper _reportMapper;

        
        public string ConnectionString
        {
            get { return _connectionString; }
        }


        public EmployeeMapper EmployeeMapper
        {
            get { return _employeeMapper; }
        }


        public PayrollMapper PayrollMapper
        {
            get { return _payrollMapper; }
        }   

        public TimeRecordMapper TimeRecordMapper
        {
            get { return _timeRecordMapper; }
        }   
        
        public ReportMapper ReportMapper
        {
            get { return _reportMapper; }
        }

        public DBService() { }

        /// <summary>
        ///  DBService constructor initializing mapper objects and connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public DBService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _employeeMapper = new EmployeeMapper();
            _payrollMapper = new PayrollMapper();
            _timeRecordMapper = new TimeRecordMapper(); 
            _reportMapper = new ReportMapper(); 
        }



        /// <summary>
        /// Method testing database connection
        /// </summary>
        /// <returns>boolean assessing databease connection</returns>
        /// <exception cref="MySqlException"></exception>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GenerateQuery
        /// Date: 2024-08-08
        /// </remarks>
        public bool IsSuccessfulConnection()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (MySqlException me)
                {
                    Console.WriteLine($"Database connection failed: {me.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database connection failed: {ex.Message}");
                    return false;
                }
            }
        }


    }
}
