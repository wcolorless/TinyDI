using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TinyDI
{
    public class TinyDependencyInjection
    {
        private readonly List<IDependency> dependencies;
        public TinyDependencyInjection()
        {
            dependencies = new List<IDependency>();
        }


        public void AddDependency(IDependency dependency)
        {
            dependencies.Add(dependency);
        }

        public void Init()
        {
            for(int i = 0; i < dependencies.Count; i++)
            {
                if(dependencies[i].CurrentObj == null)
                {
                    CreateObj.Go(dependencies, dependencies[i].Implementation);
                }
            }
        }

        public TResponse GetService<TResponse>()
        {
            IDependency dependency;
            var serviceType = typeof(TResponse);
            if(serviceType.IsInterface)
            {
                 dependency = dependencies.Find(x => x.Interface == serviceType);
            }
            else
            {
                 dependency =  dependencies.Find(x => x.Implementation == serviceType);
            }
            if (dependency == null) throw new CantFindThisDependency(typeof(TResponse).FullName);
            var obj = (TResponse)CreateObj.Go(dependencies, dependency.Implementation);
            return obj;
        }


        private object InvokeMethod(Type impType, Type cType, object command)
        {
            var dep = dependencies.Find(x => x.Implementation == impType);
            if (dep.Behaviour == DIBehaviour.Singleton)
            {
                var methodInfo = FindCommandMethod.Find(impType, cType, dependencies);
                if (methodInfo == null) throw new CantFindMethod(cType.FullName, impType.FullName);
                object obj = methodInfo.Invoke(dep.CurrentObj, new object[] { command });
                return obj;
            }
            else
            {
                var workObj = CreateObj.Go(dependencies, impType);
                dep.CurrentObj = workObj;
                var methodInfo = FindCommandMethod.Find(impType, cType, dependencies);
                if (methodInfo == null) throw new CantFindMethod(cType.FullName, impType.FullName);
                object obj = methodInfo.Invoke(dep.CurrentObj, new object[] { command });
                return obj;
            }
        }

        public  Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
             Type cType = command.GetType();
             var impType = FindCommandType.Find(cType, dependencies);
             InvokeMethod(impType, cType, command);
             return Task.CompletedTask;
        }

        public async Task<TResponse> GetAsync<TQuery, TResponse>(IQuery query) where TQuery : IQuery
        {
            return await  Task.Run(() => {
               Type cType = query.GetType();
               var impType = FindCommandType.Find(cType, dependencies);
               Task<TResponse> obj = (Task<TResponse>)InvokeMethod(impType, cType, query);
               var result = (TResponse)obj.Result;
               return result;
           });
        }

    }
}
