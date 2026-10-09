namespace PlaylistFlowbench.Domain;

/// <summary>Domain owner profile; authentication will be supplied by ASP.NET Core Identity.</summary>
public sealed class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string DisplayName { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
}
