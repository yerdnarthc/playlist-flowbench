using PlaylistFlowbench.Components.Library;
using PlaylistFlowbench.Models;

namespace PlaylistFlowbench.Services;

// Throwaway stand-in for the future database. Late Night Drive mirrors the
// Figma Overview frame exactly; the rest are plausible filler so every
// Library card navigates somewhere. Delete when integrations land.
public sealed class MockPlaylistRepository : IPlaylistRepository
{
    private readonly Dictionary<string, PlaylistOverview> _overviews;

    public MockPlaylistRepository()
    {
        var lateNightDrive = BuildLateNightDrive();
        _overviews = new Dictionary<string, PlaylistOverview>(StringComparer.OrdinalIgnoreCase)
        {
            [lateNightDrive.Slug] = lateNightDrive,
        };

        var index = 0;
        foreach (var summary in PlaylistLibraryData.Playlists)
        {
            if (!_overviews.ContainsKey(summary.Slug))
            {
                _overviews[summary.Slug] = BuildGeneratedOverview(summary, index);
            }

            index++;
        }
    }

    public IReadOnlyList<PlaylistSummary> GetPlaylists() => PlaylistLibraryData.Playlists;

    public PlaylistOverview? GetOverview(string slug) =>
        _overviews.TryGetValue(slug, out var overview) ? overview : null;

    private static PlaylistOverview BuildLateNightDrive() => new(
        Slug: "late-night-drive",
        Title: "Late Night Drive",
        Description: "Curated synthwave and downtempo progression mapped for night drives.",
        Version: "v04.2",
        SourceLabel: "LOCAL BENCH",
        TrackCountLabel: "38 tracks",
        TotalDuration: "02:44:18",
        BpmRange: "92–126 BPM",
        EnergyCurve: BuildEnergyArc(38),
        Segments: new List<EnergySegment>
        {
            new("01 Intro", "92→98 BPM"),
            new("02 Cruise", "98→112 BPM"),
            new("03 Peak", "112→126 BPM", IsPeak: true),
            new("04 Cooldown", "110→90 BPM"),
        },
        Sections: new List<SectionSummary>
        {
            BuildSection("01 Intro", "Intro", "16:42", "92 → 98 BPM", 4, new List<OverviewTrack>
            {
                new(1, "Midnight City", "M83", "11A", 105, "04:03"),
                new(2, "Nightcall", "Kavinsky", "7A", 93, "04:18"),
                new(3, "Resonance", "HOME", "8B", 92, "03:32"),
                new(4, "After Dark", "Mr.Kitty", "8A", 118, "04:17"),
            }),
            BuildSection("02 Cruise", "Cruise", "54:10", "98 → 112 BPM", 12, new List<OverviewTrack>
            {
                new(5, "Awake", "Tycho", "9A", 100, "04:43"),
                new(6, "Sunset", "The Midnight", "9A", 104, "05:26"),
                new(7, "Tech Noir", "GUNSHIP", "10B", 108, "04:57"),
            }),
            BuildSection("03 Peak", "Peak", "38:20", "112 → 126 BPM", 8, new List<OverviewTrack>
            {
                new(17, "Turbo Killer", "Carpenter Brut", "11B", 126, "03:28"),
                new(18, "Roller Mobster", "Carpenter Brut", "11B", 124, "03:34"),
            }),
            BuildSection("04 Cooldown", "Cooldown", "55:06", "110 → 90 BPM", 14, new List<OverviewTrack>
            {
                new(25, "A Walk", "Tycho", "8A", 98, "05:16"),
            }),
        });

    private static SectionSummary BuildSection(
        string name, string shortName, string durationLabel, string bpmRangeLabel,
        int trackCount, IReadOnlyList<OverviewTrack> shownTracks)
    {
        var hiddenCount = trackCount - shownTracks.Count;
        var firstHidden = shownTracks.Count > 0 ? shownTracks[^1].Position + 1 : 1;
        var lastHidden = firstHidden + hiddenCount - 1;
        var hiddenLabel = hiddenCount > 0
            ? $"+ {hiddenCount} more tracks in {shortName} (tracks {firstHidden:00} – {lastHidden:00})"
            : string.Empty;

        return new SectionSummary(name, trackCount, durationLabel, bpmRangeLabel, shownTracks, hiddenCount, hiddenLabel);
    }

    // Filler for non-Figma playlists: same shape, sample tracks, rough stats.
    private static PlaylistOverview BuildGeneratedOverview(PlaylistSummary summary, int offset)
    {
        var tracks = SampleTracks(offset);
        var sections = new List<SectionSummary>
        {
            BuildSection("01 Flow", "Flow", "48:00", "90 → 120 BPM", 20, tracks.GetRange(0, 3)),
        };

        return new PlaylistOverview(
            summary.Slug,
            summary.Title,
            "Imported playlist awaiting section mapping.",
            "v01.0",
            "LOCAL BENCH",
            summary.Meta.Split('·')[0].Trim(),
            "01:30:00",
            "90–120 BPM",
            BuildEnergyArc(20),
            new List<EnergySegment>
            {
                new("01 Flow", "90→120 BPM"),
            },
            sections);
    }

    private static List<OverviewTrack> SampleTracks(int offset)
    {
        var pool = new List<OverviewTrack>
        {
            new(0, "Midnight City", "M83", "11A", 105, "04:03"),
            new(0, "Nightcall", "Kavinsky", "7A", 93, "04:18"),
            new(0, "Resonance", "HOME", "8B", 92, "03:32"),
            new(0, "After Dark", "Mr.Kitty", "8A", 118, "04:17"),
            new(0, "Awake", "Tycho", "9A", 100, "04:43"),
            new(0, "Sunset", "The Midnight", "9A", 104, "05:26"),
        };

        var tracks = new List<OverviewTrack>();
        for (var i = 0; i < 3; i++)
        {
            var sample = pool[(offset + i) % pool.Count];
            tracks.Add(sample with { Position = i + 1 });
        }

        return tracks;
    }

    // Gentle rise then fall, peaking just past the middle — like Figma's curve.
    private static double[] BuildEnergyArc(int count)
    {
        var values = new double[count];
        for (var i = 0; i < count; i++)
        {
            var t = count == 1 ? 0 : (double)i / (count - 1);
            values[i] = 0.25 + 0.6 * Math.Sin(Math.Pow(t, 1.3) * Math.PI);
        }

        return values;
    }
}
