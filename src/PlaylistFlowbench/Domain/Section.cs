namespace PlaylistFlowbench.Domain;

/// <summary>A named contiguous run of occurrences within one playlist.</summary>
public sealed class Section
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlaylistId { get; set; }
    public required string Name { get; set; }
    public string? Notes { get; set; }
    public string? Color { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Playlist? Playlist { get; set; }
    public ICollection<PlaylistTrack> Tracks { get; set; } = new List<PlaylistTrack>();
}
