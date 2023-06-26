using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Waterschapshuis.CatchRegistration.Common.Tests
{
    public static class EmbeddedResource
    {
        public static string ReadResourceContent(string namespaceAndFileName, Assembly assembly)
        {
            try
            {
                using (Stream? stream = assembly.GetManifestResourceStream(namespaceAndFileName))
                {
                    if (stream == null)
                    {
                        throw new InvalidOperationException($@"Resource {namespaceAndFileName} could not be read from assembly: {assembly.GetName()}");
                    }
                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }

            catch (Exception exception)
            {
                throw new Exception($"Failed to read Embedded Resource {namespaceAndFileName}", exception);
            }
        }
    }
}
