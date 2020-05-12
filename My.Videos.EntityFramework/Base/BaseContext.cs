using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework.Base
{
    public class BaseContext : DbContext
    {
        /// <summary>
        /// DbConnectionString
        /// </summary>
        public string DbConnectionString { private set; get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="_dbConnectionString"></param>
        public BaseContext(string _dbConnectionString)
                : base(_dbConnectionString)
        {
            DbConnectionString = _dbConnectionString;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

    }
}
