namespace DDaS.Core.API
{
    public interface ICompilers
    {
        ICompiler GetCompiler(CompileId id);
    }
}