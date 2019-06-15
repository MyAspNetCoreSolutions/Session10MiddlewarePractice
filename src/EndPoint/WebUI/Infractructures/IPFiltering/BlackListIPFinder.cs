using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Contracts;

namespace WebUI.Infractructures
{
    public class BlackListIPFinder : IIPFinder
    {
        private readonly IConfiguration _configuration;

        public BlackListIPFinder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<string> Find()
        {
            
            string ips = _configuration["AdminBlackList"];
            if (string.IsNullOrEmpty(ips))
            {
                throw new Exception("Not BlackListIp Found in Setting file");
            }
            string[] IpAarray = ips.Split(';');
            if (IpAarray==null)
            {
                throw new Exception("Bad Ip List stracture in setting file");
            }

            return IpAarray.ToList();
        }
    }
}
