using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDaS.Core.Common;
using DDaS.Core.Models;

// ReSharper disable InvertIf

namespace DDaS.Core.Tools
{
    public static class ExecTool
    {
        public static async Task<Executed> MoveOutputToFile(this Executed exec)
        {
            if (exec is { Exit: 0, File: IExFileObj tf })
            {
                await File.WriteAllTextAsync(tf.File, exec.Out, Encoding.UTF8);
                exec = exec with { File = new TempFile(tf.File), Out = null };
            }
            return exec;
        }

        public static async Task<Executed> CollectScattered(this Executed exec, string ext, string mark)
        {
            if (exec is { Exit: 0, File: IExFileObj tf })
            {
                var dir = Path.GetDirectoryName(tf.File)!;
                const SearchOption so = SearchOption.AllDirectories;
                var files = Directory.EnumerateFiles(dir, $"*{ext}", so);
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
                await File.WriteAllLinesAsync(tf.File, allTxt, Encoding.UTF8);
                exec = exec with { File = new TempFile(tf.File) };
            }
            return exec;
        }
    }
}