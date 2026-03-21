namespace DDaS.Core.Models
{
    public record Executed(
        IFileObj File,
        int Ms,
        int Exit,
        string? Out
    );
}