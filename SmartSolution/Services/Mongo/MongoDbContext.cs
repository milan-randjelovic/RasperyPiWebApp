using MongoDB.Driver;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;

namespace WebPortal.Services.Mongo
{
    public class MongoDbContext
    {
        private MongoClient client;
        private IMongoDatabase dbContext;
        public IMongoCollection<Switch> Switches { get; set; }
        public IMongoCollection<SwitchLog> SwitchesLog { get; set; }
        public IMongoCollection<Sensor> Sensors { get; set; }
        public IMongoCollection<SensorLog> SensorsLog { get; set; }

        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }

        public MongoDbContext(string connectionString, string databaseName)
        {
            this.DatabaseConnectionString = connectionString;
            this.DatabaseName = databaseName;
            this.client = new MongoClient(this.DatabaseConnectionString);
            this.dbContext = client.GetDatabase(this.DatabaseName);
            this.Switches = dbContext.GetCollection<Switch>(Configuration.Switches);
            this.SwitchesLog = dbContext.GetCollection<SwitchLog>(Configuration.SwitchesLog);
            this.Sensors = dbContext.GetCollection<Sensor>(Configuration.Sensors);
            this.SensorsLog = dbContext.GetCollection<SensorLog>(Configuration.SensorsLog);
        }
    }
}
