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

        public TimeRecordService(DBService dbService, TimeRecordMapper timeRecordMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _timeRecordMapper = timeRecordMapper ?? throw new ArgumentNullException(nameof(timeRecordMapper));
        }

        public async Task<List<TimeRecord>> GetTimeRecords()
        {
            List<TimeRecord> timeRecordList = new List<TimeRecord>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT *  FROM TimeRecords_view", conn))
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