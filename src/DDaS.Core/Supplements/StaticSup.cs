using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DDaS.Core.Supplements
{
    internal static class StaticSup
    {
        internal static T GetEmbeddedBinary<T>(string name, Type type)
        {
            var asm = type.Assembly;
            var dir = Path.GetFullPath(asm.Location);











            throw new InvalidOperationException(dir + " | " + name);
        }
    }
}