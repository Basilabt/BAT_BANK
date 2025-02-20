using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BATBank_Tracker_Service
{
    public partial class TrackerService : ServiceBase
    {
        public TrackerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string logDirectory = @"D:\Logs";
            string logFilePath = Path.Combine(logDirectory, "BATBank_API_Service_Log.txt");
            
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - BATBank API Service Started\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        protected override void OnStop()
        {
            string logDirectory = @"D:\Logs";
            string logFilePath = Path.Combine(logDirectory, "BATBank_API_Service_Log.txt");
          
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] - BATBank API Service Stopped\n";
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
