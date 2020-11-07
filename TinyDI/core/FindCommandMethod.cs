using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TinyDI
{
    public class FindCommandMethod
    {
        public static MethodInfo Find(Type findableType, Type parameterType, List<IDependency> dependencies)
        {
            var methods = findableType.GetMethods();
            for(int i = 0; i < methods.Length; i++)
            {
                var method = methods[i];
                var parameters = method.GetParameters();
                for(int p = 0; p < parameters.Length; p++)
                {
                    if(parameters[i].ParameterType.FullName == parameterType.FullName)
                    {
                        return method;
                    }
                }
            }
            return null;
        }
    }
}
