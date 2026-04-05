using System;

namespace DDaS.Core.Common
{
    public interface ITemper
    {
        string GetTempDir(IController ctrl, Enum id);
    }
}