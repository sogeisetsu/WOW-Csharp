using System;

namespace ConsoleApp1
{
    internal class Program
    {
        /// <summary>
        /// mian方法
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestA testA = new();
            testA.FormatJson2();
        }
    }
}