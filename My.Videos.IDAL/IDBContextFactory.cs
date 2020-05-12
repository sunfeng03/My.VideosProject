using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.IDAL
{
    public interface IDBContextFactory
    {
        DbContext CreateDbContext();
    }
}
