using LocatorsTAF.CoreLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocatorsTAF.ReqnrollTests
{
    public class DriverContext
    {
        public IWebDriverWrapper DriverWrapper { get; set; } = null!;
    }
}
