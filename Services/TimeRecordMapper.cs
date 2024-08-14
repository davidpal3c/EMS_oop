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

        /// <summary>
        /// Mapper method from reader, instantiating TimeRecord object.
        /// </summary>       
        /// <typeparam name="MySqlDataReader"></typeparam>
        /// <param name="reader"></param>        
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException">throws error mapping message related to invalid types</exception>
        /// <remarks>
        /// Name: MapFromReaderTimeRecord
        /// Date: 2024-08-10       
        /// </remarks>
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


        /// <summary>
        /// Mapper method from reader, instantiating TimeRecordView object for additional fields.
        /// </summary>       
        /// <typeparam name="MySqlDataReader"></typeparam>
        /// <param name="reader"></param>        
        /// <exception cref="Exception"></exception>
        /// <exception cref="InvalidOperationException">throws error mapping message related to invalid types</exception>
        /// <remarks>
        /// Name: MapFromReaderTimeRecordReport
        /// Date: 2024-08-13        
        /// </remarks>
        public TimeRecordView MapFromReaderTimeRecordReport(MySqlDataReader reader)
        {
            try
            {
                TimeRecordView timeRecord = new TimeRecordView();

                return new TimeRecordView
                {
                    Count = reader.GetInt32("TypeCount"),
                    Title = reader.GetString("TypeName"),
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to TimeRecord object", ex);
            }

        }
    }
}
