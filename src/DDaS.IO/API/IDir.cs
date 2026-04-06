namespace DDaS.IO.API
{
    public interface IDir : IEntry
    {
        IFile GetFile(string name);

        void TrackFiles(params string[] patterns);
    }
}