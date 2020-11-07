using System;
using System.Collections.Generic;
using System.Text;

namespace TinyDI
{
    public class Dependency : IDependency
    {
        public Type Interface { get; set; }
        public Type Implementation { get; set; }
        public DIBehaviour Behaviour { get; set; } = DIBehaviour.Renewed;
        public object CurrentObj { get; set; }

        public static Dependency Create()
        {
            return new Dependency();
        }

        public Dependency For<TInterface>()
        {
            this.Interface = typeof(TInterface);
            return this;
        }

        public Dependency Use<TImplementation>()
        {
            this.Implementation = typeof(TImplementation);
            return this;
        }

        public Dependency SetBehaviour(DIBehaviour behaviour)
        {
            Behaviour = behaviour;
            return this;
        }

    }
}
