﻿using My.Videos.EntityFramework;
using My.Videos.IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.DAL.DALFactory
{
    public class DbContextFactory : IDBContextFactory
    {
        /// <summary>
        /// 保证EF上下文实例是线程内唯一。
        /// </summary>
        /// <returns></returns>
        public DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("dbContext");
            if (dbContext == null)
            {
                dbContext = new VideoDbContext();
                CallContext.SetData("dbContext", dbContext);
            }
            return dbContext;
        }
    }
}
