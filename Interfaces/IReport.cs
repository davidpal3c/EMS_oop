using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;

namespace EMS.Interfaces
{
    internal interface IReport
    {
        /*
        int ReportId { get; set; }
        string ReportName { get; set; }
        string ReportQuery { get; set; }
        Employee GeneratedBy { get; set; }
        */
        

        Task<List<Report>> GenerateReport(Report.EReportType rt);

        /*
        void SaveReport();                    
        void SendReport();
        void DeleteReport();
        */

    }
}
