using System;

namespace DDaS.IO.API
{
    public interface IEntry : IDisposable
    {
        string Name { get; }
    }
}