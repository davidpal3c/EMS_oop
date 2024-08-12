using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;


namespace EMS.Services
{
    public class ReportService
    {
        private readonly DBService _dbService;
        private readonly ReportMapper _reportMapper;

        public ReportService(DBService dbService, ReportMapper reportMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _reportMapper = reportMapper ?? throw new ArgumentNullException(nameof(reportMapper));
        }


        public async Task<List<Report>> GetReport(string reportType)
        {
            List<Report> reportList = new List<Report>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(_dbService.ConnectionString))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand(reportType, conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reportType == "EmployeeDirectory")
                            {
                                while (await reader.ReadAsync())
                                {
                                    EmployeeDirectoryReport report = _reportMapper.MapFromReaderEmpDirectoryReport(reader);
                                    reportList.Add(report);
                                }                                                               

                            }                            
                            else if (reportType == "Attendance")
                            {
                                while (await reader.ReadAsync())
                                {
                                    AttendanceReport report = _reportMapper.MapFromReaderAttendanceReport(reader);
                                    reportList.Add(report);
                                }
                            }                            
                            else if (reportType == "LeaveAndAbsence")
                            {
                                while (await reader.ReadAsync())
                                {
                                    LeaveAndAbsenceReport report = _reportMapper.MapLeaveAndAbsenceReport(reader);
                                    reportList.Add(report);
                                }
                            }                            
                            else
                            {
                                throw new Exception("Invalid report type");
                            }

                            return reportList;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }
            
        }
    }
}
