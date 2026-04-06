using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Tools;
using DDaS.Tools.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDaS.Runner.Core
{
    public static class DbgTool
    {
        public static T Cast<T>(this OkObjectResult oor)
        {
            var val = oor.Value;
            var res = (T)val!;
            return res;
        }

        public static string ToStr(ToolInfo ti)
        {
            var line = $"[{ti.Id}] {ti.Name} v{ti.Version} ({ti.Year})";
            return line;
        }

        public static void Print(IEnumerable<ToolInfo> infos)
        {
            foreach (var info in infos)
            {
                Console.WriteLine($" * {ToStr(info)}");
            }
        }

        public static bool IsOk(IActionResult kind, out string? err)
        {
            if (kind is BadRequestObjectResult bor)
            {
                err = $" ({bor.StatusCode}) {bor.Value}";
                return false;
            }
            err = null;
            return true;
        }

        public static IFileX? GetFileObj(string? input)
        {
            if (GetFile(input) is not { } path) return null;
            var name = Path.GetFileName(path);
            var bytes = File.ReadAllBytes(path);
            const string mime = Defaults.Octet;
            return Files.NewMemFile(name, bytes, mime, null!);
        }

        public static string? GetFile(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            var path = Path.GetFullPath(input);
            return File.Exists(path) ? path : null;
        }

        public static IFormFile? ToFormFile(this IFileX? file)
        {
            return file?.Bytes.AsFile(file.Name, file.Mime);
        }

        public static void Print(Executed res)
        {
            Console.WriteLine($" ### duration = {res.Ms} ms ; exit code = {res.Exit}");
            if (!string.IsNullOrWhiteSpace(res.Out))
            {
                Console.WriteLine(" --- BEGIN OUTPUT ---");
                Console.WriteLine(res.Out.Trim());
                Console.WriteLine(" ---  END OUTPUT  ---");
            }
            var ext = Path.GetExtension(res.File.Name);
            switch (ext)
            {
                case ".asm":
                case ".s":
                    Console.WriteLine(Encoding.UTF8.GetString(res.File.Bytes));
                    break;
                case ".com":
                    Console.WriteLine(WriteNumberedFile(res.File));
                    break;
                default:
                    throw new InvalidOperationException($"'{ext}'!");
            }
        }

        private static string WriteNumberedFile(IFileX file)
        {
            if (file.Bytes.Length == 0) return string.Empty;
            var bse = Path.GetFileNameWithoutExtension(file.Name);
            var ext = Path.GetExtension(file.Name);
            var dir = Environment.CurrentDirectory;
            var i = 1;
            string outName;
            while (File.Exists(outName = Path.Combine(dir, $"{bse}.{i}.{ext.TrimStart('.')}"))) i++;
            File.WriteAllBytes(outName, file.Bytes);
            return outName;
        }
    }
}