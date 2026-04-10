using System;
using System.Collections.Generic;
using DDaS.IO.API;

namespace DDaS.Core.Models
{
    public record Executed(
        IFile File,
        int Ms,
        int Exit,
        string? Out
    );
}