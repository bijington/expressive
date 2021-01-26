using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace Expressive.Tests
{
    public static class VariableProviderDictionaryTests
    {
        [Test]
        public static void TestTryGetValue()
        {
            var provider = new Mock<IVariableProvider>();
            var dictionary = new VariableProviderDictionary(provider.Object);

            dictionary.TryGetValue("", out _);

            provider.Verify(p => p.TryGetValue(It.IsAny<string>(), out It.Ref<object>.IsAny), Times.Once);
        }

        [Test]
        public static void TestUnsupported()
        {
            var dictionary = new VariableProviderDictionary(Mock.Of<IVariableProvider>());

            Assert.That(() => dictionary.Count, Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.IsReadOnly, Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Keys, Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Values, Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Add("", new object()), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Add(new KeyValuePair<string, object>("", new object())), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Clear(), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Contains(new KeyValuePair<string, object>("", new object())), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.ContainsKey(""), Throws.InstanceOf<NotSupportedException>());
#pragma warning disable CA1825
            Assert.That(() => dictionary.CopyTo(new KeyValuePair<string, object>[0], 0), Throws.InstanceOf<NotSupportedException>());
#pragma warning restore CA1825
            Assert.That(() => dictionary.GetEnumerator(), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => ((IEnumerable)dictionary).GetEnumerator(), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Remove(""), Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary.Remove(new KeyValuePair<string, object>("", new object())), Throws.InstanceOf<NotSupportedException>());

            Assert.That(() => dictionary[""], Throws.InstanceOf<NotSupportedException>());
            Assert.That(() => dictionary[""] = new object(), Throws.InstanceOf<NotSupportedException>());
        }
    }
}
