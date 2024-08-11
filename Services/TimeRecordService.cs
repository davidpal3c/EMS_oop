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




    }
}
