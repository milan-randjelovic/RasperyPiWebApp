using MongoDB.Driver;
using WebPortal.Models;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core;

namespace WebPortal.Services.Mongo
{
    public class MongoDbContext: IDbContext
    {
        private MongoClient client;
        private IMongoDatabase dbContext;
        public IMongoCollection<Switch> Switches { get; set; }
        public IMongoCollection<SwitchLog> SwitchesLog { get; set; }
        public IMongoCollection<Sensor> Sensors { get; set; }
        public IMongoCollection<SensorLog> SensorsLog { get; set; }
        public IMongoCollection<User> Users { get; set; }
        public IMongoCollection<User> UsersAppending { get; set; }

        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }

        public MongoDbContext(ApplicationConfiguration configuration)
        {
            this.DatabaseConnectionString = configuration.DatabaseConnection;
            this.DatabaseName = configuration.DatabaseName;
            this.client = new MongoClient(this.DatabaseConnectionString);
            this.dbContext = client.GetDatabase(this.DatabaseName);
            this.Switches = dbContext.GetCollection<Switch>(configuration.Switches);
            this.SwitchesLog = dbContext.GetCollection<SwitchLog>(configuration.SwitchesLog);
            this.Sensors = dbContext.GetCollection<Sensor>(configuration.Sensors);
            this.SensorsLog = dbContext.GetCollection<SensorLog>(configuration.SensorsLog);
            this.Users = dbContext.GetCollection<User>(configuration.Users);
            this.UsersAppending = dbContext.GetCollection<User>(configuration.UsersAppending);
        }
    }
}
