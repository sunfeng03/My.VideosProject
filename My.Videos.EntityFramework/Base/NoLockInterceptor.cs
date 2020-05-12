using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace My.Videos.EntityFramework.Base
{
    public class NoLockInterceptor : DbCommandInterceptor
    {
        private static readonly Regex TableAliasRegex =
     new Regex(@"(?<tableAlias>AS \[Extent\d+\](?! WITH \(NOLOCK\)))", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        /// <summary>
        /// IsEnableNoLock   是否使用 WITH (NOLOCK)
        /// </summary>
        //[ThreadStatic]
        public static bool IsEnableNoLock;

        /// <summary>
        /// Scalars the executing.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="interceptionContext">The interception context.</param>
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            if (!IsEnableNoLock) return;
            command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");
        }

        /// <summary>
        /// Readers the executing.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="interceptionContext">The interception context.</param>
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            if (!IsEnableNoLock) return;
            command.CommandText = TableAliasRegex.Replace(command.CommandText, "${tableAlias} WITH (NOLOCK)");

        }
    }
}
