namespace PlaylistFlowbench.Domain;

/// <summary>One occurrence of a track; repeated songs have separate occurrence IDs.</summary>
public sealed class PlaylistTrack
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlaylistId { get; set; }
    public Guid TrackId { get; set; }
    /// <summary>One-based playback order, independent of canvas coordinates.</summary>
    public int Position { get; set; }
    public Guid? SectionId { get; set; }
    public bool IsLocked { get; set; }
    /// <summary>Optional per-occurrence energy in the range 0 through 1.</summary>
    public double? UserEnergyOverride { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public Playlist? Playlist { get; set; }
    public Track? Track { get; set; }
    public Section? Section { get; set; }
}
