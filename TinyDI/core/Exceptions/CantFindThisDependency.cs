using System;
using System.Collections.Generic;
using System.Text;

namespace TinyDI
{
    public class CantFindThisDependency : Exception
    {
        public CantFindThisDependency(string findType) : base(string.Format("CantFindThisDependency for {0} in IoC", findType))
        {

        }
    }
}
