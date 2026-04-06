namespace DDaS.IO.API
{
    public interface IDirX : IEntryX
    {
        IFileX GetFile(string name);

        void TrackFiles(params string[] patterns);
    }
}