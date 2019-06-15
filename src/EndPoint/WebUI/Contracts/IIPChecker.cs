using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebUI.Contracts
{
    public interface IIPChecker
    {
        bool IsBlackIp(IPAddress ip);
    }
}
