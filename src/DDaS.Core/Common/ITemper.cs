using DDaS.IO.API;

namespace DDaS.Core.Common
{
    public interface ITemper
    {
        IDir CreateTmpDir(object sender, object id);
    }
}