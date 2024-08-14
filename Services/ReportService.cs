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
        

        public DBService DBService
        {
            get { return _dbService; }
        }


        /// <summary>
        /// Constructor for ReportService class, instanting DBService and ReportMapper objects to be used in the class.
        /// </summary>
        /// <param name="dbService"></param>
        /// <param name="reportMapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ReportService(DBService dbService, ReportMapper reportMapper)
        {
            _dbService = dbService ?? throw new ArgumentNullException(nameof(dbService));
            _reportMapper = reportMapper ?? throw new ArgumentNullException(nameof(reportMapper));
        }


        /// <summary>
        /// Async method generating query to fetch reports from the database.
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <param name="query">component provided query string from user selection</param>
        /// <returns>A task async returning list of report objects mapped from the database using the sort string</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="MySqlException"></exception>
        /// <remarks>
        /// Name: GetReport
        /// Date: 2024-08-12
        /// </remarks>
        public async Task<List<Report>> GetReport(string query, Report.EReportType reportType)
        {

            List<Report> reportList = new List<Report>();


            //temporary cnnection string fix, _dbService.ConnectionString is null?
            string connectionStr = "Server=107.180.27.178;Port=3306;Database=employeemanager;Uid=oopadmin;Pwd=taQoCt]ApQZ5;Connection Timeout=30";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionStr))
                {
                    await conn.OpenAsync();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {

                            // switch statement to determine which report type to map from reader to object
                            switch (reportType)
                            {
                                case Report.EReportType.EmployeeDirectory:

                                    while (await reader.ReadAsync())
                                    {
                                        //returns retrieved mapped object as an instantiated object to add to list
                                        EmployeeDirectoryReport report = _reportMapper.MapFromReaderEmpDirectoryReport(reader);
                                        reportList.Add(report);
                                    }

                                    break;


                                case Report.EReportType.Attendance:
                                   
                                    while (await reader.ReadAsync())
                                    {
                                        AttendanceReport report = _reportMapper.MapFromReaderAttendanceReport(reader);
                                        reportList.Add(report);
                                    }

                                    break;

                            
                                case Report.EReportType.LeaveAndAbsence:
                                
                                    while (await reader.ReadAsync())
                                    {
                                        LeaveAndAbsenceReport report = _reportMapper.MapLeaveAndAbsenceReport(reader);
                                        reportList.Add(report);
                                    }

                                    break;

                                default:    
                                    throw new Exception("Invalid report type");
                            }
                                                        
                        }
                    }
                }
            }
            catch (MySqlException sqlEx)
            {                
                throw new Exception($"SQL Error fetching reports: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching payrolls: {ex.Message}");
            }

            return reportList;

        }
    }
}
