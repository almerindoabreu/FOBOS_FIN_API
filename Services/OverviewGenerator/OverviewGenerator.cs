using IronPython.Hosting;
using IronPython.Runtime;
using IronPython;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using System.Diagnostics;
using FOBOS_API.Utils;
using System;
using System.Threading.Tasks;

namespace FOBOS_API.Services.OverviewGenerator
{
    public class OverviewGenerator
    {
        private string lastelyFeedback { get; set; }
        public OverviewGenerator()
        {
            lastelyFeedback = lastelyFeedback;
        }

        public async Task InvoiceOfTheMonth()
        {
            Process process = new Process();
            ProcessStartInfo batFile = new ProcessStartInfo();
            try
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.FileName = "cmd.exe";
                process.Start();

                //batFile.FileName = @"D:\Projetos\FOBOS_FIN_API\Services\OverviewGenerator\Script\invoice_of_the_month.bat";
                batFile.UseShellExecute = true;
                batFile.FileName = ServerApp.MapPath("Services/OverviewGenerator/script/invoice_of_the_month.bat");
                Process.Start(batFile);
            }catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void DailyOverview()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(@"Services/OverviewGenerator/script/daily-overview.py");
        }

        public async Task WeeklyOverview()
        {
            Process process = new Process();
            ProcessStartInfo batFile = new ProcessStartInfo();
            try
            {
                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.CreateNoWindow = true;
                //process.StartInfo.RedirectStandardInput = true;
                //process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.RedirectStandardError = true;
                //process.StartInfo.FileName = "cmd.exe";
                //process.Start();

                batFile.UseShellExecute = true;
                batFile.FileName = ServerApp.MapPath("Services/OverviewGenerator/script/weekly-overview.bat");
                Process.Start(batFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void MonthlyOverview()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(@"monthly-overview.py");
        }

        public void QuaterOverview()
        {
            ScriptEngine engine = Python.CreateEngine();
            engine.ExecuteFile(@"quarter-overview.py");
        }
    }
}
