using DDaS.Core.Tools;
using DDaS.IO.API;
using DDaS.IO.Temp;
using Microsoft.Extensions.Logging;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBeMadeStatic.Global

namespace DDaS.Core.Common
{
    public sealed class Temper : ITemper
    {
        private readonly ILogger<Temper> _log;
        private readonly string _tmpRoot;

        public Temper(ILogger<Temper> log)
        {
            _log = log;
            var tmpRoot = FileTool.GetEnvVarPath("DDAS_TMP", "tmp");
            _tmpRoot = FileTool.CreateOrGetDir(tmpRoot)!;
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Temporary root is '{Root}'", _tmpRoot);
        }

        public IDir CreateTmpDir(object sender, object id)
        {
            var dir = new TmpDir(_tmpRoot);
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Created temp for '{Obj}' '{Id}' => {Dir}", sender.GetType().Name, id, dir.Real);
            return dir;
        }
    }
}