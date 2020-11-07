using System;
using System.Collections.Generic;
using System.Text;

namespace TinyDI
{
    public class CantFindMethod : Exception
    {
        public CantFindMethod(string methodName, string objName) : base(string.Format("CantFindMethod {0} in {1} type", methodName, objName))
        {

        }
    }
}
