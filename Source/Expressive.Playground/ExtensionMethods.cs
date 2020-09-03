using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Expressive.Playground
{
    public static class ExtensionMethods
    {
        public static string SafeExtractAttributeValue(this XElement element, string attributeName)
        {
            return element.SafeExtractAttributeValue<string>(attributeName, (s) => s);
        }

        public static T SafeExtractAttributeValue<T>(this XElement element, string attributeName, Func<string, T> typeConversion)
        {
            return element.SafeExtractAttributeValue(attributeName, typeConversion, default(T));
        }

        public static T SafeExtractAttributeValue<T>(this XElement element, string attributeName, Func<string, T> typeConversion, T defaultValue)
        {
            T returnVal = defaultValue;

            XAttribute labelAttribute = element.Attribute(XName.Get(attributeName));

            if (labelAttribute != null && !String.IsNullOrWhiteSpace(labelAttribute.Value))
            {
                returnVal = typeConversion(labelAttribute.Value);
            }

            return returnVal;
        }
    }
}
