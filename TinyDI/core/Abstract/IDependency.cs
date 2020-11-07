using System;
using System.Collections.Generic;
using System.Text;

namespace TinyDI
{
    public enum DIBehaviour
    {
        Singleton,
        Renewed
    }



    public interface IDependency
    {
        DIBehaviour Behaviour { get; set; }
        Type Interface { get; set; }
        Type Implementation { get; set; }
        object CurrentObj { get; set; }
    }
}
