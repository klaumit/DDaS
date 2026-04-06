using DDaS.IO.API;

namespace DDaS.Core.Models
{
    public record Executed(
        IFileX File,
        int Ms,
        int Exit,
        string? Out
    );
}