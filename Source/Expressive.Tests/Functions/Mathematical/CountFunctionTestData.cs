using System.Collections.Generic;
using System.Linq;
using Expressive.Functions.Mathematical;

namespace Expressive.Tests.Functions.Mathematical
{
    /// <summary>
    /// Simple test data class for verifying the <see cref="CountFunction"/>.
    /// </summary>
    public class CountFunctionTestData
    {
        public int ExpectedCount { get; }

        public IEnumerable<object> Values { get; }

        public CountFunctionTestData(int expectedCount, params object[] values)
        {
            this.ExpectedCount = expectedCount;
            this.Values = values;
        }

        public override string ToString() => $"Expected {this.ExpectedCount} from {string.Join(", ", this.Values.Select(v => v?.ToString() ?? "null"))}";
    }
}
