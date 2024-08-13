using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;
using EMS.Services;
using EMS.Interfaces;


namespace EMS.Models
{
    public class Report : IReport
    {
        private int _reportId;
        private string _reportName;
        private EReportType _type;
        private string _query;
        private DateTime _createdDate;
        private Employee generatedBy;


        public enum EReportType
        {
            EmployeeDirectory,
            Attendance,
            LeaveAndAbsence
        }


        public int ReportId
        {
            get { return _reportId; }
            set { _reportId = value; }
        }

        public string ReportName
        {
            get { return _reportName; }
            set { _reportName = value; }
        }

        public EReportType ReportQuery
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Query
        {
           get { return _query; }
            set { _query = value; }
        }   


        public Employee GeneratedBy
        {
            get { return generatedBy; }
            set { generatedBy = value; }
        }


        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public Report() { }

        public static string GenerateQuery(EReportType reportType)
        {
            string query;

            switch (reportType)
            {
                case EReportType.EmployeeDirectory:

                    return "SELECT Id, Name, Email, Position, Salary, CreatedAt, UpdatedAt FROM Employees";                    

                case EReportType.Attendance:

                    return "SELECT e.Id, e.Name, t.Date, t.HoursWorked, e.Email, e.Position, t.CreatedAt FROM Employees e JOIN TimeRecords t ON e.Id = t.EmployeeId WHERE t.Type = 'Attendance'";                    

                case EReportType.LeaveAndAbsence:

                    return "SELECT e.Id, e.Name, t.Date, t.HoursWorked, e.Email, e.Position, t.CreatedAt FROM Employees e JOIN TimeRecords t ON e.Id = t.EmployeeId WHERE (t.Type = 'Leave' OR t.Type = 'Absence')";
                    

                default:
                    throw new ArgumentException("Invalid report type");
            }
        }

        public async Task<List<Report>> GenerateReport(EReportType reportType)
        {
            try
            {
                ReportService reportService = new ReportService(new DBService(), new ReportMapper());                            
                return await reportService.GetReport(GenerateQuery(reportType), reportType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error generating report", ex);
            }
        }

        
        public async Task SaveReport<T>(List<T> repList, Report.EReportType repType)
        {
            IOService ioService = new IOService();

            string repTitle = repType.ToString();
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            string dateStr = date.ToString();

            await ioService.ExportToXls<T>(repList, $@"C:\cprg211\{repTitle}-{dateStr}.xlsx", $"{repTitle}", repType);
        }
        


        /*
        public void SendReport();
        
        
        */
    }
}
