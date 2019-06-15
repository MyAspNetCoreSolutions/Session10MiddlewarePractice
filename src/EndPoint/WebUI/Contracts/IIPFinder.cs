using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Contracts
{
    public interface IIPFinder
    {
        List<string> Find();
    }
}
