using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebPortal
{
    public class ApplicationConfiguration
    {
        public DataBase DataBase { get; set; }
        public string ServerPort { get; set; }
        public string DatabaseConnection { get; set; }
        public string DatabaseName { get; set; }
        public string Sensors { get; set; }
        public string SensorsLog { get; set; }
        public string Switches { get; set; }
        public string SwitchesLog { get; set; }
        public string Users { get; set; }
        public bool LoggingEnabled { get; set; }
        public int LoggingInterval { get; set; }
        public string APIBaseAddress { get; set; }

        /// <summary>
        /// Save configuration
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName = "ApplicationConfiguration.xml")
        {
            try
            {
                using (var writer = new StreamWriter(fileName))
                {
                    var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                    serializer.Serialize(writer, this);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load configuration
        /// </summary>
        public void Load(string fileName = "ApplicationConfiguration.xml")
        {
            try
            {
                if (File.Exists(fileName))
                {
                    using (var stream = File.OpenRead(fileName))
                    {
                        var serializer = new XmlSerializer(typeof(ApplicationConfiguration));
                        ApplicationConfiguration config = serializer.Deserialize(stream) as ApplicationConfiguration;
                        this.ServerPort = config.ServerPort;
                        this.APIBaseAddress = config.APIBaseAddress;
                        this.DataBase = config.DataBase;
                        this.DatabaseName = config.DatabaseName;
                        this.DatabaseConnection = config.DatabaseConnection;
                        this.Sensors = config.Sensors;
                        this.SensorsLog = config.SensorsLog;
                        this.Switches = config.Switches;
                        this.SwitchesLog = config.SwitchesLog;
                        this.Users = config.Users;
                        this.LoggingEnabled = config.LoggingEnabled;
                        this.LoggingInterval = config.LoggingInterval;
                    }
                }
                else
                {
                    Ininitialize();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Initialize config
        /// </summary>
        public void Ininitialize()
        {
            this.ServerPort = "8080";
            this.APIBaseAddress = "http://localhost:" + ServerPort + "/api";
            this.DataBase = DataBase.SQLite;
            this.DatabaseName = "SmartSolution";
            switch (DataBase)
            {
                case DataBase.SQLite:
                    this.DatabaseConnection = "Data Source = SmartSolution.db";
                    break;
                case DataBase.MongoDB:
                    this.DatabaseConnection = "mongodb://SmartSolution:SmartSolution2017@35.160.134.78:19735/SmartSolution";
                    break;
                default:
                    this.DatabaseConnection = "Data Source = SmartSolution.db";
                    break;
            }
            this.Sensors = "Sensors";
            this.SensorsLog = "SensorsLog";
            this.Switches = "Switches";
            this.SwitchesLog = "SwitchesLog";
            this.Users = "Users";
            this.LoggingEnabled = true;
            this.LoggingInterval = 1000;
            this.Save();
        }
    }

    public enum DataBase
    {
        SQLite,
        MongoDB
    }
}
