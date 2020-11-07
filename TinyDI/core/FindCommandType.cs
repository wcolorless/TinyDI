using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TinyDI
{
    public class FindCommandType
    {
        public static Type Find(Type findableType, List<IDependency> dependencies)
        {
            var types = dependencies.Select(t => t.Implementation).ToList();
            for(int i = 0; i < types.Count; i++)
            {
                var currentType = types[i];
                var Methods = currentType.GetMethods().ToList();
                for(int m = 0; m < Methods.Count; m++)
                {
                    var method = Methods[m];
                    var args = method.GetParameters();
                    for(int p = 0; p < args.Length; p++)
                    {
                        if(args[p].ParameterType.FullName == findableType.FullName)
                        {
                            return currentType;
                        }
                    }
                }
            }
            throw new CantFindThisDependency(findableType.FullName);
        }
    }
}
