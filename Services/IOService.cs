using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using EMS.Models;

namespace EMS.Services
{
    public class IOService
    {
        //handles the export to xls logic according to report type
        public async Task ExportToXls<T>(List<T> list, string path, string sheetName, Report.EReportType repType)
        {
            IXLWorkbook workbook = new XLWorkbook();
                       
            try
            {
                switch (repType)
                {
                    case Report.EReportType.EmployeeDirectory:
                        var employeeList = list.OfType<EmployeeDirectoryReport>().ToList();
                        workbook = await ExportEmployeeDirectoryToXls(employeeList, path, sheetName);
                        break;

                    case Report.EReportType.Attendance:
                        var attendanceList = list.OfType<AttendanceReport>().ToList();
                        workbook = await ExportAttendanceToXls(attendanceList, path, sheetName);
                        break;

                    case Report.EReportType.LeaveAndAbsence:
                        var leaveAndAbsenceList = list.OfType<LeaveAndAbsenceReport>().ToList();
                        workbook = await ExportLeaveAndAbsenceToXls(leaveAndAbsenceList, path, sheetName);
                        break;

                    default:
                        throw new ArgumentException("Invalid report type specified.", nameof(repType));
                }

                await SaveWorkbookAsync(workbook, path);
            }
            catch (Exception ex)
            {
                throw new Exception("Error exporting to xlsx.", ex);
            }   
                
        }

        private async Task<IXLWorkbook> ExportEmployeeDirectoryToXls(List<EmployeeDirectoryReport> list, string file, string sheetName)
        {
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);

            string[] headers = new[]
            {
                "Id", "Name", "Email", "Position", "Salary", "CreatedAt", "UpdatedAt"
            };

            // adds headers to the first row
            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            // adds data to the worksheet
            for (int i = 0; i < list.Count; i++)
            {
                EmployeeDirectoryReport employee = list[i];
                worksheet.Cell(i + 2, 1).Value = employee.Id;
                worksheet.Cell(i + 2, 2).Value = employee.Name;
                worksheet.Cell(i + 2, 3).Value = employee.Email;
                worksheet.Cell(i + 2, 4).Value = employee.Position;
                worksheet.Cell(i + 2, 5).Value = employee.Salary;
                worksheet.Cell(i + 2, 6).Value = employee.CreatedAt.ToString("yyyy-MM-dd");
                worksheet.Cell(i + 2, 7).Value = employee.UpdatedAt.ToString("yyyy-MM-dd");
            }

            return await Task.FromResult(workbook);
        }        

        private async Task<IXLWorkbook> ExportAttendanceToXls(List<AttendanceReport> list, string file, string sheetName)
        {
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);

            string[] headers = new[]
            {
                "Id", "Name", "Date", "HoursWorked", "Email", "Position", "CreatedAt"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            for (int i = 0; i < list.Count; i++)
            {
                var attendance = list[i];
                worksheet.Cell(i + 2, 1).Value = attendance.Id;
                worksheet.Cell(i + 2, 2).Value = attendance.Name;
                worksheet.Cell(i + 2, 3).Value = attendance.Date.ToString("yyyy-MM-dd");
                worksheet.Cell(i + 2, 4).Value = attendance.HoursWorked;
                worksheet.Cell(i + 2, 5).Value = attendance.Email;
                worksheet.Cell(i + 2, 6).Value = attendance.Position;
                worksheet.Cell(i + 2, 7).Value = attendance.CreatedAt.ToString("yyyy-MM-dd");
            }

            return await Task.FromResult(workbook);
        }

        private async Task<IXLWorkbook> ExportLeaveAndAbsenceToXls(List<LeaveAndAbsenceReport> list, string file, string sheetName)
        {
            XLWorkbook workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add(sheetName);

            string[] headers = new[]
            {
                "Id", "Name", "Date", "HoursWorked", "Email", "Position", "CreatedAt"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = headers[i];
            }

            for (int i = 0; i < list.Count; i++)
            {
                var leave = list[i];
                worksheet.Cell(i + 2, 1).Value = leave.Id;
                worksheet.Cell(i + 2, 2).Value = leave.Name;
                worksheet.Cell(i + 2, 3).Value = leave.Date.ToString("yyyy-MM-dd");
                worksheet.Cell(i + 2, 4).Value = leave.HoursWorked;
                worksheet.Cell(i + 2, 5).Value = leave.Email;
                worksheet.Cell(i + 2, 6).Value = leave.Position;
                worksheet.Cell(i + 2, 7).Value = leave.CreatedAt.ToString("yyyy-MM-dd");
            }

            return await Task.FromResult(workbook);
        }

        private async Task SaveWorkbookAsync(IXLWorkbook workbook, string file)
        {
            await Task.Run(() => workbook.SaveAs(file));
        }
    }

}
