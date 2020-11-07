using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace TinyDI
{
    public class CreateObj
    {
        public static object Go(List<IDependency> dependencies, Type type)
        {
            var cList = new List<ConstructorInfo>();
            var constructors = type.GetConstructors().Where(x => x.GetParameters().Length > 0).ToList();
            if(constructors.Count == 0)
            {
                var dep = dependencies.Find(x => x.Implementation.FullName == type.FullName || x.Interface.FullName == type.FullName);
                if (dep == null) throw new CantFindThisDependency(type.FullName);
                if(dep.Behaviour == DIBehaviour.Singleton && dep.CurrentObj != null)
                {
                    return dep.CurrentObj;
                }
                dep.CurrentObj = Activator.CreateInstance(dep.Implementation);
                return dep.CurrentObj;
            }
            else
            {
                var constructor = constructors[0];
                var parameters = constructor.GetParameters();
                var allParams = new object[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    var obj = Go(dependencies, parameters[i].ParameterType);
                    allParams[i] = obj;
                }
                var dep = dependencies.Find(x => x.Implementation.FullName == type.FullName);
                dep.CurrentObj = Activator.CreateInstance(dep.Implementation, allParams);
                return dep.CurrentObj;
            }
        }
    }
}
