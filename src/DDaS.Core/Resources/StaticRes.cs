using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DDaS.Core.Resources
{
    internal static class StaticRes
    {
        internal static T GetEmbeddedJson<T>(string name, Type type)
        {
            var asm = type.Assembly;
            var nsp = type.Namespace;
            var fqn = nsp + "." + name;
            using var stream = asm.GetManifestResourceStream(fqn)!;
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var json = reader.ReadToEnd();
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj!;
        }

        internal static string FindFile(Type type)
        {
            var asm = type.Assembly;
            var path = Path.GetFullPath(asm.Location);
            var dir = Path.GetDirectoryName(path);
            return dir!;
        }
    }
}