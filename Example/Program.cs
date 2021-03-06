﻿using System;
using Example.core;
using TinyDI;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var di = new TinyDependencyInjection();
            di.AddDependency(Dependency.Create().For<ITestWriter>().Use<TestWriter>());
            di.Init();

            var testWriter = di.GetService<TestWriter>();
            var testWriterFromType = di.GetService(typeof(TestWriter)) as TestWriter;
            testWriter.PrintHelloWorld();
            testWriterFromType.PrintHelloWorld();
        }
    }
}
