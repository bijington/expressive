using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Expressive.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        [TestMethod]
        public void Test()
        {
            var times = new Test(10000, 1).Execute();

            var average = times.First();

            Trace.WriteLine($"Evaluation took: {average}");
        }
    }

    public class Test
    {
        private readonly int numberCount;
        private readonly int evaluationCount;

        public Test(int numberCount, int evaluationCount)
        {
            this.numberCount = numberCount;
            this.evaluationCount = evaluationCount;
        }

        public IEnumerable<TimeSpan> Execute()
        {
            var result = new List<TimeSpan>();
            var builder = new StringBuilder("(");

            for (var i = 0; i < this.numberCount; i++)
            {
                if (builder.Length > 0)
                {
                    builder.Append(" + ");
                }

                builder.Append($"{i}");
            }

            builder.Append(")");

            var expression = new Expression(builder.ToString());

            var stopwatch = new Stopwatch();

            for (var i = 0; i < this.evaluationCount; i++)
            {
                stopwatch.Start();
                var r = expression.Evaluate();
                stopwatch.Stop();

                NUnit.Framework.Assert.That(r, Is.EqualTo(49995000));
                result.Add(stopwatch.Elapsed);
                stopwatch.Reset();
            }

            return result;
        }
    }
}
