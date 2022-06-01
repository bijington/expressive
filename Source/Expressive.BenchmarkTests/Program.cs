using BenchmarkDotNet.Running;
using Expressive.BenchmarkTests;
using System;

namespace Expressive.BenchmarkTests
{
    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<PerformanceTests>();

            Console.WriteLine(summary);
        }
    }
}