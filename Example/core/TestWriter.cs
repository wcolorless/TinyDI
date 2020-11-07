using System;
using System.Collections.Generic;
using System.Text;

namespace Example.core
{
    public interface ITestWriter
    {
        void PrintHelloWorld();
    }


    public class TestWriter
    {
        public void PrintHelloWorld()
        {
            Console.WriteLine("Hello World");
        }
    }
}
