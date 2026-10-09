namespace PlaylistFlowbench.Domain;

/// <summary>A user's editable, linear arrangement of track occurrences.</summary>
public sealed class Playlist
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? SourceType { get; set; }
    public string? ExternalId { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public User? Owner { get; set; }
    public ICollection<PlaylistTrack> Tracks { get; set; } = new List<PlaylistTrack>();
    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
