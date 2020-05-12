using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace My.Videos.Common.Utils
{
    public class EnvironmentUtil
    {
        /// <summary>
        /// The _environment ip
        /// </summary>
        private static string _environmentIp;

        /// <summary>
        /// 获取当前主机多个IP
        /// </summary>      
        public static string EnvironmentIp
        {
            get
            {
                if (!string.IsNullOrEmpty(_environmentIp)) return _environmentIp;
                try
                {
                    string hostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                    IPAddress[] addr = ipEntry.AddressList;
                    var ips = addr.Where(o => !o.IsIPv6LinkLocal).ToList();
                    List<string> lastIp = new List<string>();
                    ips.ForEach(t =>
                    {
                        var ip = t.ToString();
                        if (ip.Contains(":")) return;
                        lastIp.Add(ip);
                    });
                    _environmentIp = string.Join("|", lastIp);
                }
                catch (Exception)
                {
                    _environmentIp = "R" + new Random().Next(255).ToString("d3") + "." + new Random().Next(255).ToString("d3") + "." + new Random().Next(255).ToString("d3");
                }
                return _environmentIp;
            }
        }
    }
}
