using PlaylistFlowbench.Components.Library;
using PlaylistFlowbench.Models;

namespace PlaylistFlowbench.Services;

// Read-model seam for playlist data. Pages depend on this interface, never on
// the mock below — when EF Core lands, only the registration in Program.cs changes.
public interface IPlaylistRepository
{
    IReadOnlyList<PlaylistSummary> GetPlaylists();

    PlaylistOverview? GetOverview(string slug);
}
