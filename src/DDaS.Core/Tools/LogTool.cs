using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace DDaS.Core.Tools
{
    public static class LogTool
    {
        public static Dictionary<T, ILogger> CreateAll<T>(this ILoggerFactory factory, Type type)
            where T : struct, Enum
        {
            var logs = new Dictionary<T, ILogger>();
            var tName = type.FullName!.TrimEnd('s');
            foreach (var id in Enum.GetValues<T>())
                logs[id] = factory.CreateLogger($"{tName}<{id}>");
            return logs;
        }
    }
}