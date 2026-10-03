using PlaylistFlowbench.Models;

namespace PlaylistFlowbench.Components.Library;

// Mocked library content matching the Figma Playlist Library frame.
// Replaced by real import data once integrations land (see docs/context.md §7.1).
public static class PlaylistLibraryData
{
    public static IReadOnlyList<PlaylistSummary> Playlists { get; } = new List<PlaylistSummary>
    {
        new("Late Night Drive", "38 tracks · 2h 44m", "Edited 12m ago", "#3b2b4d", "#1c1828"),
        new("Coding at 2AM", "52 tracks · 3h 52m", "Edited 3h ago", "#27383a", "#151f21"),
        new("Midnight Run", "24 tracks · 1h 31m", "Edited Yesterday", "#452d27", "#1e1514"),
        new("Neon Rain", "31 tracks · 2h 14m", "Edited 2 days ago", "#472234", "#1d121b"),
        new("Quiet Sunday", "19 tracks · 1h 12m", "Edited 4 days ago", "#25362e", "#131b17"),
        new("After Hours", "45 tracks · 3h 15m", "Edited May 18", "#382b40", "#1a1420"),
    };
}
