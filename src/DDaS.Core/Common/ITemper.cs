using System;
using DDaS.Core.Assemblers.API;

namespace DDaS.Core.Common
{
    public interface ITemper
    {
        string GetTempDir(IController ctrl, Enum id);
    }
}