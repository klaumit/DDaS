namespace DDaS.Core.Models
{
    public record Compiled(
        IFileObj File,
        int Ms,
        int Exit,
        string? Out,
        string? Err
    );
}