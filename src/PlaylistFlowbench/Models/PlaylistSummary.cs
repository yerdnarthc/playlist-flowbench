namespace PlaylistFlowbench.Models;

// Display data for one card on the Playlist Library screen.
// Values mirror the Figma frame text exactly; artwork colors are the
// thumbnail gradients from Figma (no image assets in the prototype yet).
public sealed record PlaylistSummary(
    string Title,
    string Meta,
    string EditedAgo,
    string ArtworkFrom,
    string ArtworkTo);
