using DDaS.Core.Tools;
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

        public ITempDir CreateTmpDir(object sender, object id)
        {
            if (_log.IsEnabled(LogLevel.Debug))
                _log.LogDebug("Creating temp dir for '{Obj}' '{Id}'", sender.GetType().Name, id);
            return new TempDir(_tmpRoot);
        }
    }
}