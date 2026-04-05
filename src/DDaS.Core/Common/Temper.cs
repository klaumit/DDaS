using System;
using System.IO;
using DDaS.Core.Tools;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace DDaS.Core.Common
{
    public sealed class Temper : ITemper
    {
        private static readonly string TmpDir = FileTool.CreateOrGetDir("tmp")!;

        public string GetTempDir(IController obj, Enum id)
        {
            const string tmp = "Controller";
            var name = obj.GetType().Name;
            name = name.Replace(tmp, string.Empty);
            name = name.ToLowerInvariant().Substring(0, 3);
            var idt = id.ToString();
            idt = idt.ToLowerInvariant();
            var hash = Random.Shared.Next().ToString("x8");
            var path = Path.Combine(TmpDir, name, idt, hash);
            path = FileTool.CreateOrGetDir(path)!;
            return path;
        }
    }
}