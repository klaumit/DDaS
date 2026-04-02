using System;
using System.Collections.Generic;
using DDaS.Core.Models;
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
    }
}