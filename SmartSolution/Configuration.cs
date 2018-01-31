using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal
{
    public class Configuration
    {
        public static string DatabaseConnection
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    //return "mongodb://SmartSolution:SmartSolution2017@localhost:19735/SmartSolution";
                    return "mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution";
                }
                else
                {
                    return "mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution";
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
    }
}
