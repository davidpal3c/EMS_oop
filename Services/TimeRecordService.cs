using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;
using MySqlConnector;

namespace EMS.Services
{
    public class TimeRecordService
    {
        private readonly DBService _dbService;
        private readonly TimeRecordMapper _timeRecordMapper;


        /// <summary>
        /// Constructor for TimeRecordService class, instanting DBService and TimeRecordMapper objects to be used in the class.
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="timeRecordMapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public TimeRecordService(DBService dbService, TimeRecordMapper timeRecordMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _timeRecordMapper = timeRecordMapper ?? throw new ArgumentNullException(nameof(timeRecordMapper));
        }



        /// <summary>
        /// Async method generating query to fetch time records from the database.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="sort">component provided sorting string</param>
        /// <returns>A task async returning list of TimeRecord objects mapped from the database using the sort string</returns>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GetTimeRecords
        /// Date: 2024-08-11
        /// </remarks>
        public async Task<List<TimeRecord>> GetTimeRecords(string sort)
        {
            List<TimeRecord> timeRecordList = new List<TimeRecord>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT *  FROM TimeRecords_view order by {sort}", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                TimeRecord timeRecord = _timeRecordMapper.MapFromReaderTimeRecord(reader);
                                timeRecordList.Add(timeRecord);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return timeRecordList;
        }


        /// <summary>
        /// Async method generating query to fetch time record report view (1) from the database.
        /// </summary>        
        /// <param>No parameters</param>
        /// <returns>Asynchronous task returning a list of time records with more information from virtual table</returns>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: GetEmployees
        /// Date: 2024-08-13
        /// Used to retrieve other fields from the database virtual table.
        /// </remarks>
        public async Task<List<TimeRecordView>> GetTimeRecordsReport()
        {
            List<TimeRecordView> timeRecordList = new List<TimeRecordView>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT *  FROM TimeRecords_report1", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                TimeRecordView timeRecord = _timeRecordMapper.MapFromReaderTimeRecordReport(reader);
                                timeRecordList.Add(timeRecord);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return timeRecordList;
        }



        /// <summary>
        /// Async method generating query to fetch TimeRecords based user search parameter. 
        /// </summary>       
        /// <typeparam name="string"></typeparam>
        /// <param name="search"></param>
        /// <returns>Asynchronous task returning a list of TimeRecords objects from search query.>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: SearchEmployees
        /// Date: 2024-08-11      
        /// </remarks>
        public async Task<List<TimeRecord>> SearchTimeRecords(string search)
        {
            List<TimeRecord> timeRecordList = new List<TimeRecord>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM TimeRecords_view where Name like '%{search}%'", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                TimeRecord timeRecord = _timeRecordMapper.MapFromReaderTimeRecord(reader);
                                timeRecordList.Add(timeRecord);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return timeRecordList;
        }

        /// <summary>
        /// Async method generating query to fetch employees ordered based on user given filter type, and sort parameter. 
        /// </summary>       
        /// <typeparam name="string"></typeparam>
        /// <param name="field, string, sort"></param>
        /// <returns>Asynchronous task returning a list of time record objects from filter query.>
        /// <exception cref="Exception"></exception>
        /// <remarks>
        /// Name: FilterTimeRecords
        /// Date: 2024-08-13       
        /// </remarks>
        public async Task<List<TimeRecord>> FilterTimeRecords(string field, string search)
        {
            List<TimeRecord> timeRecordList = new List<TimeRecord>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand($"SELECT * FROM TimeRecords_view where {field} = '{search}'", conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                TimeRecord timeRecord = _timeRecordMapper.MapFromReaderTimeRecord(reader);
                                timeRecordList.Add(timeRecord);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return timeRecordList;
        }


        /// <summary>
        /// Async method generating inserting time record object in database
        /// </summary>       
        /// <typeparam name="object"></typeparam>
        /// <param name="List"></param>        
        /// <remarks>
        /// Name: AddTimeRecord
        /// Date: 2024-08-11
        /// Parametizes insert query to add employee data into the database, using index of objects in list.
        /// </remarks>
        public async Task AddTimeRecord(List<object> timeRecordData)
        {

            if (timeRecordData == null || !(timeRecordData.Count == 4))
                throw new ArgumentException("Invalid payroll data");

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = $"INSERT INTO TimeRecords (EmployeeId, Date, HoursWorked, Type) VALUES (@EmployeeId, @Date, @HoursWorked, @Type);";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", timeRecordData[0]);
                    cmd.Parameters.AddWithValue("@Date", timeRecordData[1]);
                    cmd.Parameters.AddWithValue("@HoursWorked", timeRecordData[2]);
                    cmd.Parameters.AddWithValue("@Type", timeRecordData[3]);                    

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }


        /// <summary>
        /// Asynchronous task updatig time record field into database
        /// </summary>       
        /// <typeparam name="Payroll"></typeparam>
        /// <param name="updatedPayroll"></param>        
        /// <remarks>
        /// Name: UpdateEmployee
        /// Date: 2024-08-11  
        /// Method used to update employee fields in the database table 'Employees', based on employee id. 
        /// Parametizes update data in the database using index of objects in list.
        public async Task UpdateTimeRecord(TimeRecord updatedTimeRecord)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    string commandString = "UPDATE TimeRecords SET EmployeeId = @EmployeeId, Date = @Date, HoursWorked = @HoursWorked, Type = @Type WHERE EmployeeId = @EmployeeId";

                    using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeId", updatedTimeRecord.EmployeeId);
                        cmd.Parameters.AddWithValue("@Date", updatedTimeRecord.Date);
                        cmd.Parameters.AddWithValue("@HoursWorked", updatedTimeRecord.HoursWorked);
                        cmd.Parameters.AddWithValue("@Type", updatedTimeRecord.Type);

                        cmd.Parameters.AddWithValue("@Id", updatedTimeRecord.Id);

                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                if (updatedTimeRecord == null)
                    throw new ArgumentException("Invalid time record data");

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating time record: {ex.Message}");
            }            
        }


        /// <summary>
        /// Asynchronous task deleting timeRecord field from database
        /// </summary>       
        /// <typeparam name="TimeRecord"></typeparam>
        /// <param name="timeRecord"></param>        
        /// <remarks>
        /// Name: DeleteEmployee
        /// Date: 2024-08-11 
        /// Method used to delete timer ecord field in the database table 'TimeRecord', based on time record id. 
        /// </remarks>
        public async Task DeleteTimeRecord(TimeRecord timeRecord)
        {

            using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
            {
                await conn.OpenAsync();

                string commandString = "DELETE FROM TimeRecords WHERE EmployeeId = @EmployeeId";

                using (MySqlCommand cmd = new MySqlCommand(commandString, conn))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", timeRecord.EmployeeId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }

    }
}