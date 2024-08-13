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
               

        Task<List<Report>> GenerateReport(Report.EReportType rt);
                
        Task SaveReport<T>(List<T> repList, Report.EReportType rt);                    
        
        
    }
}
