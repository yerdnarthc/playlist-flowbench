namespace PlaylistFlowbench.Domain;

/// <summary>Platform-neutral song metadata shared by playlist occurrences.</summary>
public sealed class Track
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ExternalSource { get; set; }
    public string? ExternalId { get; set; }
    public required string Title { get; set; }
    public required string Artist { get; set; }
    public string? Album { get; set; }
    public TimeSpan Duration { get; set; }
    public string? ArtworkUrl { get; set; }
    public decimal? Bpm { get; set; }
    public string? MusicalKey { get; set; }
    /// <summary>Optional normalized energy in the range 0 through 1.</summary>
    public double? Energy { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    public ICollection<PlaylistTrack> Occurrences { get; set; } = new List<PlaylistTrack>();
}
