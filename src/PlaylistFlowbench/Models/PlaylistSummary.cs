namespace PlaylistFlowbench.Models;

// Display data for one card on the Playlist Library screen.
// Values mirror the Figma frame text exactly; artwork colors are the
// thumbnail gradients from Figma (no image assets in the prototype yet).
// Slug is the route key for /playlists/{slug} (stands in for the future DB id).
public sealed record PlaylistSummary(
    string Slug,
    string Title,
    string Meta,
    string EditedAgo,
    string ArtworkFrom,
    string ArtworkTo);
