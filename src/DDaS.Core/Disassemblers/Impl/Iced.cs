using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDaS.Core.Disassemblers.API;
using DDaS.Core.Models;
using Iced.Intel;
using DO = Iced.Intel.DecoderOptions;

namespace DDaS.Core.Disassemblers.Impl
{
    public sealed class Icey : IDisassembler
    {
        public Task<Executed> Disassemble(IFileObj input)
        {
            throw new NotImplementedException();
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
                yield return $"{instr.IP:X4} {output.ToStringAndReset()}";
            }
        }
    }
}