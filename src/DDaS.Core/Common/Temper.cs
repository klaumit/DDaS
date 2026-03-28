using System;
using System.IO;
using DDaS.Core.Tools;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace DDaS.Core.Common
{
    public class Temper
    {
        private static readonly string TmpDir = FileTool.CreateOrGetDir("tmp")!;

        public string GetTempDir<TO, TI>(TO obj, TI id)
        {
            const string tmp = "Controller";
            var name = obj?.GetType().Name ?? string.Empty;
            name = name.Replace(tmp, string.Empty);
            name = name.ToLowerInvariant().Substring(0, 3);
            var idt = id?.ToString() ?? string.Empty;
            idt = idt.ToLowerInvariant();
            var hash = Random.Shared.Next().ToString("x8");
            var path = Path.Combine(TmpDir, name, idt, hash);
            path = FileTool.CreateOrGetDir(path)!;
            return path;
        }
    }
}