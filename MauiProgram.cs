using Microsoft.Extensions.Logging;
using EMS.Services;

namespace EMS
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            builder.Services.AddTransient<DBService>(provider  =>
            {
                string connectionString = "Server=107.180.27.178;Port=3306;Database=employeemanager;Uid=oopadmin;Pwd=taQoCt]ApQZ5";
                return new DBService(connectionString);
            });


            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<EmployeeService>();
            builder.Services.AddSingleton<EmployeeMapper>();
            builder.Services.AddSingleton<PayrollService>();
            builder.Services.AddSingleton<PayrollMapper>();
            builder.Services.AddSingleton<TimeRecordService>();
            builder.Services.AddSingleton<TimeRecordMapper>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
