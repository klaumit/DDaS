using System;

namespace DDaS.Core.API
{
    public interface IFileObj : IDisposable
    {
        string Name { get; }

        byte[] Bytes { get; }
    }
}