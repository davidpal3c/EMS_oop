﻿using MySqlConnector;
using System;
using EMS.Models;

namespace EMS.Services
{
    public class DBService
    {
        private readonly string _connectionString;
        private readonly EmployeeMapper _employeeMapper;
        //private readonly PayrollMapper _payrollMapper;
        //private readonly TimeRecordMapper _timeRecordMapper;


        public string ConnectionString
            {
                get { return _connectionString; }
            }


        public EmployeeMapper EmployeeMapper
        {
            get { return _employeeMapper; }
        }


        public DBService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            _employeeMapper = new EmployeeMapper();
        }


        // Connection reference
        public bool IsSuccessfulConnection()
        {
            using (var conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
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