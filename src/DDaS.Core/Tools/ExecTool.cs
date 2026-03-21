using System.IO;
using System.Text;
using System.Threading.Tasks;
using DDaS.Core.Models;

// ReSharper disable InvertIf

namespace DDaS.Core.Tools
{
    public static class ExecTool
    {
        public static async Task<Executed> MoveOutputToFile(this Executed exec)
        {
            if (exec is { Exit: 0, File: TempFile tf })
            {
                await File.WriteAllTextAsync(tf.File, exec.Out, Encoding.UTF8);
                exec = exec with { File = new TempFile(tf.File), Out = null };
            }
            return exec;
        }
    }
}