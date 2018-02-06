using System;

namespace WebPortal
{
    public class Configuration
    {
        public static DataBase DataBase
        {
            get
            {
                return DataBase.SQLite;
            }
        }
        public static string ServerPort
        {
            get
            {
                return "8080";
            }
        }
        public static string DatabaseConnection
        {
            get
            {
                switch (DataBase)
                {
                    case DataBase.SQLite:
                        return "Data Source = SmartSolution.db";
                    case DataBase.MongoDB:
                        return "mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution";
                    default:
                        return "Data Source = SmartSolution.db";
                }
            }
        }
        public static string DatabaseName
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    return "SmartSolution";
                }
                else
                {
                    return "SmartSolution";
                }
            }
        }
        public static string Sensors
        {
            get
            {
                return "Sensors";
            }
        }
        public static string SensorsLog
        {
            get
            {
                return "SensorsLog";
            }
        }
        public static string Switches
        {
            get
            {
                return "Switches";
            }
        }
        public static string SwitchesLog
        {
            get
            {
                return "SwitchesLog";
            }
        }
        public static int LogInterval
        {
            get
            {
                return 1000;
            }
        }
        public static string APIBaseAddress
        {
            get
            {
                return "http://localhost:" + ServerPort + "/api";
            }
        }
    }

    public enum DataBase
    {
        SQLite,
        MongoDB
    }
}
