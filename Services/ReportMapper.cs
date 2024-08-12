using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using EMS.Models;

namespace EMS.Services
{
    public class ReportMapper
    {
        public EmployeeDirectoryReport MapFromReaderEmpDirectoryReport(MySqlDataReader reader)
        {
            try
            {
                EmployeeDirectoryReport report = new EmployeeDirectoryReport();

                return new EmployeeDirectoryReport
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Email = reader.GetString("Email"),
                    Position = reader.GetString("Position"),
                    Salary = reader.GetInt32("Salary"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                    UpdatedAt = reader.GetDateTime("UpdatedAt"),
                };
             
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to Report", ex);
            }

        }

        public AttendanceReport MapFromReaderAttendanceReport(MySqlDataReader reader)
        {
            try
            {
                AttendanceReport report = new AttendanceReport();

                return new AttendanceReport
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Date = reader.GetDateTime("CreatedAt"),
                    HoursWorked = reader.GetInt32("HoursWorked"),
                    Email = reader.GetString("Email"),
                    Position = reader.GetString("Position"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),                    
                };

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to Report", ex);
            }

        }


        public LeaveAndAbsenceReport MapLeaveAndAbsenceReport(MySqlDataReader reader)
        {
            try
            {
                LeaveAndAbsenceReport report = new LeaveAndAbsenceReport();

                return new LeaveAndAbsenceReport
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Date = reader.GetDateTime("CreatedAt"),
                    HoursWorked = reader.GetInt32("HoursWorked"),
                    Email = reader.GetString("Email"),
                    Position = reader.GetString("Position"),
                    CreatedAt = reader.GetDateTime("CreatedAt"),
                };

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error mapping data from reader to Report", ex);
            }

        }

    }
}
