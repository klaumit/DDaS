using System;

namespace DDaS.IO.API
{
    public interface IEntryX : IDisposable
    {
        string Name { get; }
    }
}