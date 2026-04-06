using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Memory;
using DDaS.IO.Tools;
using Iced.Intel;
using Microsoft.Extensions.Logging;
using Decoder = Iced.Intel.Decoder;
using DO = Iced.Intel.DecoderOptions;

namespace DDaS.Core.Disassemblers.Impl
{
    public sealed class Icey : IDisassembler
    {
        private readonly ILogger _log;

        public Icey(ILogger log)
        {
            _log = log;
        }

        public Task<Executed> Disassemble(IFile input)
        {
            var res = DisassembleSync(input);
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Executed '{Exe}' in {Run} with status {Code}!", nameof(Iced),
                    TimeSpan.FromMilliseconds(res.Ms), res.Exit);
            return Task.FromResult(res);
        }

        private static Executed DisassembleSync(IFile input)
        {
            var watch = Stopwatch.StartNew();
            var bytes = input.Bytes;
            var bld = new StringBuilder();
            bld.AppendLine();
            foreach (var line in Decode(bytes))
            {
                bld.AppendLine(line);
            }
            bld.AppendLine();
            var lis = Encoding.UTF8.GetBytes(bld.ToString());
            var output = input.GetNewNamed(Defaults.SymExt).WriteTo(lis);
            const string? warn = null;
            const int exit = 0;
            var ms = (int)watch.ElapsedMilliseconds;
            return new Executed(output, ms, exit, warn);
        }

        private static IEnumerable<string> Decode(byte[] bytes)
        {
            const DO opt = DO.NoInvalidCheck | DO.NoPause;
            var reader = new ByteArrayCodeReader(bytes);
            const ulong ip = 0;
            var decoder = Decoder.Create(16, reader, ip, opt);
            var formatter = new NasmFormatter
            {
                Options = { SpaceAfterOperandSeparator = true }
            };
            var output = new StringOutput();
            while (reader.CanReadByte)
            {
                decoder.Decode(out var instr);
                formatter.Format(instr, output);
                var subBytes = bytes.Skip((int)instr.IP).Take(instr.Length);
                var hexBytes = string.Join(" ", subBytes.Select(b => b.ToString("X2")));
                yield return $"{instr.IP:X4}   {hexBytes,-14}   {output.ToStringAndReset()}";
            }
        }
    }
}