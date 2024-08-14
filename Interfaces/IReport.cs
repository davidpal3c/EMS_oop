using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Models;

namespace EMS.Interfaces
{
    //interface for report servicing
    internal interface IReport
    {               

        Task<List<Report>> GenerateReport(Report.EReportType rt);
                
        Task SaveReport<T>(List<T> repList, Report.EReportType rt);                   
        
        
    }
}
