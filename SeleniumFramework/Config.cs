using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Personify.Helpers;
using SeleniumFramework.Helpers;

namespace SeleniumFramework
{
    public static class Config
    {
        //static Config()
        //{
        //    Load();
        //}

        public static string AppPath { get; private set; } //
        public static string ProjectPath { get; private set; } //  set below in code
        public static string ConnectionString { get; set; } //Connection string for the test environment DB set in the app.config
        public static string LauncherPath { get; private set; } //Path to BackOffice app set in the app.config 
        public static string WebUrl { get; set; } //Test Environment URL set in app.config
        public static string WebUrlCommittee { get; private set; }
        public static string EnvironmentDb { get; private set; }
        public static string Browser { get; set; }
        public static string AppTitle { get; private set; }
        public static string DefaultWait { get; private set; }
        public const string TestDataFolder = "TestData//";
        public static string SSOConnectionString { get; private set; } //Connection string for the test environment DB set in the app.config
        public static string MobileDevices { get; private set; }
        public static string PerfectoUserName { get; private set; }
        public static string PerfectoPassword { get; private set; }
        public static string PerfectoWebUrl { get; private set; }
        public static string OrgId { get; private set; }
        public static string OrgUnitId { get; private set; }
        public static string FileSaveLocation { get; private set; }
        public static string DemoPageBaseUrl { get; private set; }
        public static TimeSpan DefaultTimeSpan { get; private set; }
        public static TimeSpan NetLagWait { get; private set; }
        public static string AdminUrl { get; private set; }
        public static string[] PageErrors = {"404 - File or directory not found","Problem loading page"};



        //Changes done by Nitin - adding Novus username and password for admin user, reading values from App.config
        public static string UserName { get; private set; }
        public static string Password { get; private set; }

        public static string NovusEbiz_Url { get; private set; }
        public static bool IsTestAgent { get; set; }
        //adding constants For Dictionar object for Communication record
        public static string ComContactType = "ContactType";
        public static string ComLocationType = "LocationType";
        public static string ComComments = "Comments";
        public static string ComRecordValue = "Recordvalue";
        public static string ComExpRecordValue = "ExpRecordvalue";
        public static string ComLocationTypeCode = "LocationTypeCode";
        public static string WebDesignerConnectionString;
        public static bool LoggingOn;
        public static string JavascriptsFolder => $"{Directory.GetCurrentDirectory()}";//"C:\\AutomationFiles\\JavaScripts\\";//
        //public const string TestDataFolder = "C:\\Users\\PersonifyTFS\\Desktop\\Debug\\TestData\\";
        //public static string TestDataFolder { get; private set; }

        /// <summary>
        /// Loads app settings and constants.
        /// </summary>
        /// 
        public static void Load()
        {
            DefaultWait = ConfigurationManager.AppSettings["DefaultWait"];
            AppPath = ConfigurationManager.AppSettings["App_Path"];
            ProjectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            SqlHelper.ConnectionString = ConnectionString;
            LauncherPath = ConfigurationManager.AppSettings["Launcher_Path"];
            WebUrl = ConfigurationManager.AppSettings["WebUrl"];
            AdminUrl = ConfigurationManager.AppSettings["AdminUrl"];
            EnvironmentDb = ConfigurationManager.AppSettings["Environment_DB"];
            Browser = ConfigurationManager.AppSettings["Browser"];
            AppTitle = ConfigurationManager.AppSettings["AppTitle"];
            SSOConnectionString = ConfigurationManager.AppSettings["SSOConnectionString"];
            UserName = ConfigurationManager.AppSettings["UserName"];
            Password = ConfigurationManager.AppSettings["Password"];
            MobileDevices = "3A7F6C2BC554B6F042A9D9C32F194C4A694A98DA";
            PerfectoPassword = ConfigurationManager.AppSettings["PerectoPassword"];
            PerfectoUserName = ConfigurationManager.AppSettings["PerfectoUserName"];
            PerfectoWebUrl = ConfigurationManager.AppSettings["PerfectoWebUrl"];
            DemoPageBaseUrl = ConfigurationManager.AppSettings["DemoPageBaseUrl"];
            NovusEbiz_Url = ConfigurationManager.AppSettings["NovusEbiz_Url"];
            FileSaveLocation = ConfigurationManager.AppSettings["FileSaveLocation"];
            IsTestAgent = Convert.ToBoolean(ConfigurationManager.AppSettings["IsTestAgent"]);
            LoggingOn = Convert.ToBoolean(ConfigurationManager.AppSettings["LoggingOn"]);
            WebDesignerConnectionString = ConfigurationManager.AppSettings["WebDesignerConnectionString"];
            //string orgId = "";
            //string orgUnitId = "";
            //MyHelper.GetDefaultOrganisationDetails(out orgId, out orgUnitId);
            //OrgId = orgId;
            //OrgUnitId = orgUnitId;
            NetLagWait = TimeSpan.FromMilliseconds(Convert.ToInt32(ConfigurationManager.AppSettings["NetLagWait"]));
       
            DefaultTimeSpan  = TimeSpan.FromSeconds(Convert.ToInt32(DefaultWait));
            WaitHelpers.DefaultWait = DefaultTimeSpan;
            
            if (Browser == "Mobile" )
            {
                WebUrl = PerfectoWebUrl;
            }
        }


        public enum ModuleName
        {
            CRM360,
            Committee,
            Order,
            AccountingBatch,
            Meeting,
            Reporting
        }
    }
}
