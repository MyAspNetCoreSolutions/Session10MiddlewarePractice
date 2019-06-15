using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebUI.Contracts;

namespace WebUI.Infractructures
{
    public class IpChecker : IIPChecker
    {
        private readonly IIPFinder _iPFinder;

        public IpChecker(IIPFinder iPFinder)
        {
            this._iPFinder = iPFinder;
        }
        public bool IsBlackIp(IPAddress ip)
        {
            List<string> vs= _iPFinder.Find();

            if (vs.Count()==0 || vs==null)
            {
                return false;
            }

            

            

            var bytes = ip.GetAddressBytes();
            
            foreach (var address in vs)
            {
                var testIp = IPAddress.Parse(address);
                if (testIp.GetAddressBytes().SequenceEqual(bytes))
                {
                    return true;
                    
                }
            }

            return false;
        }
    }
}
