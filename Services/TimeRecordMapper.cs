using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;


namespace EMS.Services
{
    public class TimeRecordMapper
    {
        public TimeRecord MapFromReaderTimeRecord(MySqlDataReader reader)
        {
            try
            {
                TimeRecord timeRecord = new TimeRecord();

                return new TimeRecord
                {
                    Id = reader.GetInt32("Id"),
                    EmployeeId = reader.GetInt32("EmployeeId"),
                    EmployeeName = reader.GetString("Name"),
                    Date = reader.GetDateTime("Date"),
                    HoursWorked = reader.GetDouble("HoursWorked"),
                    Type = reader.GetString("Type"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.GetDateTime("UpdatedAt"),
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to TimeRecord object", ex);
            }

        }
    }
}
