namespace PlaylistFlowbench.Models;

// One row in a section on the Playlist Overview screen.
// Bpm is nullable: integrations may omit it (see docs/context.md §31).
public sealed record OverviewTrack(
    int Position,
    string Title,
    string Artist,
    string MusicalKey,
    int? Bpm,
    string Duration);

// A section block on the Overview screen. Only the first few tracks render;
// HiddenCount feeds the "+ N more tracks" footer row from Figma.
public sealed record SectionSummary(
    string Name,
    int TrackCount,
    string DurationLabel,
    string BpmRangeLabel,
    IReadOnlyList<OverviewTrack> ShownTracks,
    int HiddenCount,
    string HiddenLabel);

// One label under the energy curve (e.g. "03 Peak", "112→126 BPM").
// IsPeak marks the copper-accented segment from Figma.
public sealed record EnergySegment(
    string Name,
    string BpmLabel,
    bool IsPeak = false);

// Everything the Playlist Overview screen renders.
// EnergyCurve holds one normalized (0..1) value per track.
public sealed record PlaylistOverview(
    string Slug,
    string Title,
    string Description,
    string Version,
    string SourceLabel,
    string TrackCountLabel,
    string TotalDuration,
    string BpmRange,
    IReadOnlyList<double> EnergyCurve,
    IReadOnlyList<EnergySegment> Segments,
    IReadOnlyList<SectionSummary> Sections);
