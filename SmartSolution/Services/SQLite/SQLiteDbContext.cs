using Microsoft.EntityFrameworkCore;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;

namespace WebPortal.Services.SQLite
{
    public class SQLiteDbContext : DbContext
    {
        private string dbName;
        private string dbConnectionString;

        public DbSet<Switch> Switches { get; set; }
        public DbSet<SwitchLog> SwitchesLog { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorLog> SensorsLog { get; set; }

        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }

        public SQLiteDbContext(string connectionString,string databaseName)
        {
            this.dbConnectionString = connectionString;
            this.dbName = databaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.dbConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Switch>().HasKey(s => s.Id);
            modelBuilder.Entity<SwitchLog>().HasKey(s => s.Id);
            modelBuilder.Entity<Sensor>().HasKey(s => s.Id);
            modelBuilder.Entity<SensorLog>().HasKey(s => s.Id);
        }
    }
}
