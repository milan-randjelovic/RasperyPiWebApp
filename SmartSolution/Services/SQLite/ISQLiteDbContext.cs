using WebPortal.Services.Core;

namespace WebPortal.Services.SQLite
{
    public interface ISQLiteDbContext : IDbContext
    {
        int SaveChanges();
    }
}
