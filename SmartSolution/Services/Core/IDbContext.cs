namespace WebPortal.Services.Core
{
    public interface IDbContext
    {
        string DatabaseName { get; set; }
        string DatabaseConnectionString { get; set; }
    }
}
