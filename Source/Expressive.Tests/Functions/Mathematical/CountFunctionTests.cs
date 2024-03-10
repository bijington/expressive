using System.Collections.Generic;
using System.Linq;
using Expressive.Functions;
using Expressive.Functions.Mathematical;
using NUnit.Framework;

namespace Expressive.Tests.Functions.Mathematical
{
    public static class CountFunctionTests
    {
        [TestCaseSource(nameof(CountFunctionData))]
        public static void TestEvaluate(CountFunctionTestData testData)
        {
#pragma warning disable CA1062 // Validate arguments of public methods - this really isn't going to be null
            Assert.That(Evaluate(CreateFunction(), testData.Values.ToArray()), Is.EqualTo(testData.ExpectedCount));
#pragma warning restore CA1062
        }

        private static IEnumerable<CountFunctionTestData> CountFunctionData
        {
            get
            {
                yield return new CountFunctionTestData(1, 123);
                yield return new CountFunctionTestData(1, "something");
                yield return new CountFunctionTestData(1, '1');
                yield return new CountFunctionTestData(1, true);
                yield return new CountFunctionTestData(1, (object)null);

                yield return new CountFunctionTestData(2, 123, 123);
                yield return new CountFunctionTestData(2, "something", "something");
                yield return new CountFunctionTestData(2, '1', '1');
                yield return new CountFunctionTestData(2, true, true);
                yield return new CountFunctionTestData(2, null, null);

                yield return new CountFunctionTestData(3, new [] { 123, 123 }, new [] {"hello"});
                yield return new CountFunctionTestData(3, new [] { 0x10, 0x11, 0x12 });
                yield return new CountFunctionTestData(6, new List<int> { 1, 2, 3, 4, 5, 6});
                yield return new CountFunctionTestData(3, new Dictionary<int, int> { [1] = 2, [3] = 4, [5] = 6 });
            }
        }

        private static IFunction CreateFunction() => new CountFunction();

        private static object Evaluate(IFunction function, params object[] values)
        {
            return function.Evaluate(
                values.Select(v => MockExpression.ThatEvaluatesTo(v)).ToArray(),
                new Context(ExpressiveOptions.None));
        }
    }
}
