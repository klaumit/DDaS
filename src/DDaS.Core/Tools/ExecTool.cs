using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDaS.Core.Models;
using DDaS.IO.Temp;
using DDaS.IO.Tools;

// ReSharper disable InvertIf

namespace DDaS.Core.Tools
{
    public static class ExecTool
    {
        public static async Task<Executed> MoveOutputToFile(this Executed exec)
        {
            if (exec is { Exit: 0, File: { } tf })
            {
                await tf.WriteTo([exec.Out ?? string.Empty]);
                exec = exec with { File = tf, Out = null };
            }
            return exec;
        }

        public static async Task<Executed> CollectScattered(this Executed exec, string ext, string mark)
        {
            if (exec is { Exit: 0, File: { } tf })
            {
                var dir = (tf.Dir as TmpDir)!;
                dir.TrackFiles("*.res");
                dir.TrackFiles("*.bat");
                var real = dir.Real;
                const SearchOption so = SearchOption.AllDirectories;
                var files = Directory.EnumerateFiles(real, $"*{ext}", so);
                var dict = new SortedDictionary<int, string[]>();
                foreach (var file in files)
                {
                    var lines = await File.ReadAllLinesAsync(file, Encoding.UTF8);
                    foreach (var line in lines)
                    {
                        if (!line.StartsWith(mark)) continue;
                        var lineNo = line.Split(mark, 2).Last().Split('+', 2).First();
                        if (!int.TryParse(lineNo, out var lNo)) continue;
                        dict.Add(lNo, lines);
                        break;
                    }
                }
                var allTxt = dict.Values.SelectMany(v => v).Concat([""]);
                await tf.WriteTo(allTxt);
                exec = exec with { File = tf };
            }
            return exec;
        }
    }
}