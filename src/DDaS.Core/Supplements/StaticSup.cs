using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DDaS.Core.Supplements
{
    internal static class StaticSup
    {
        internal static IDictionary<string, byte[]> GetEmbeddedSet(Enum cat, string key, Type? type = null)
        {
            var typ = type ?? typeof(StaticSup);
            var asm = typ.Assembly;
            var dll = Path.GetFullPath(asm.Location);
            var dir = Path.GetDirectoryName(dll) ?? "";
            var nsp = typ.Namespace?.Split('.').Last() ?? "";
            var sub = Path.Combine(nsp, $"{cat}", key);
            var full = Path.Combine(dir, sub);
            var dict = new Dictionary<string, byte[]>();
            foreach (var file in Directory.EnumerateFiles(full, "*"))
            {
                var k = Path.GetFileName(file);
                var v = File.ReadAllBytes(file);
                dict[k] = v;
            }
            return dict;
        }
    }
}