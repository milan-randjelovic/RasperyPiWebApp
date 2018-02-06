using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Services.Core
{
    public interface IDbContext
    {
        string DatabaseName { get; set; }
        string DatabaseConnectionString { get; set; }
    }
}
