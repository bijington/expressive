using System.Collections.Generic;

namespace Expressive.Tests
{
    public class DictionaryEnumerationTestData
    {
        public ExpressiveOptions Options { get; }
        public IEqualityComparer<string> Comparer { get; }
        public bool ExpectedEnumeration { get; }

        public DictionaryEnumerationTestData(ExpressiveOptions options, IEqualityComparer<string> comparer, bool expectedEnumeration)
        {
            this.Options = options;
            this.Comparer = comparer;
            this.ExpectedEnumeration = expectedEnumeration;
        }
    }
}
