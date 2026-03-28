using System;

namespace DDaS.Core.Models
{
    public interface IFileObj : IDisposable
    {
        string Name { get; }

        byte[] Bytes { get; }

        string Mime { get; }
    }
}