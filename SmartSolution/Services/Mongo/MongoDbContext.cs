using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;

namespace WebPortal.Services.Mongo
{
    public class MongoDbContext
    {
        private string dbConnectionString;
        private string dbName;

        private MongoClient client;
        private IMongoDatabase dbContext;
        public IMongoCollection<Switch> Switches { get; set; }
        public IMongoCollection<SwitchLog> SwitchesLog { get; set; }
        public IMongoCollection<Sensor> Sensors { get; set; }
        public IMongoCollection<SensorLog> SensorsLog { get; set; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            this.dbConnectionString = connectionString;
            this.dbName = databaseName;
            this.client = new MongoClient(this.dbConnectionString);
            this.dbContext = client.GetDatabase(this.dbName);
            this.Switches = dbContext.GetCollection<Switch>(Configuration.Switches);
            this.SwitchesLog = dbContext.GetCollection<SwitchLog>(Configuration.SwitchesLog);
            this.Sensors = dbContext.GetCollection<Sensor>(Configuration.Sensors);
            this.SensorsLog = dbContext.GetCollection<SensorLog>(Configuration.SensorsLog);
        }
    }
}
