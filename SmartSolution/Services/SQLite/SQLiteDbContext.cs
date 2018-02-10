using Microsoft.EntityFrameworkCore;
using WebPortal.Models.Sensors;
using WebPortal.Models.Switches;
using WebPortal.Services.Core;

namespace WebPortal.Services.SQLite
{
    public class SQLiteDbContext : DbContext, ISQLiteDbContext
    {
        public DbSet<Switch> Switches { get; set; }
        public DbSet<SwitchLog> SwitchesLog { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorLog> SensorsLog { get; set; }

        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }

        public SQLiteDbContext(ApplicationConfiguration configurtion)
        {
            this.DatabaseConnectionString = configurtion.DatabaseConnection;
            this.DatabaseName = configurtion.DatabaseName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.DatabaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Switch>().HasKey(s => s.Id);
            modelBuilder.Entity<SwitchLog>().HasKey(s => s.Id);
            modelBuilder.Entity<Sensor>().HasKey(s => s.Id);
            modelBuilder.Entity<SensorLog>().HasKey(s => s.Id);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
