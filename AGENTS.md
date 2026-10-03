# AGENTS.md — playlist-flowbench

## Stack
- C# + ASP.NET Core + Blazor, `net10.0`, `Nullable` + `ImplicitUsings` enabled (`src/PlaylistFlowbench/PlaylistFlowbench.csproj`).
- Blazor **Interactive Server only** (`AddInteractiveServerComponents` in `Program.cs`). No WASM, no controllers, no EF Core yet.
- Styling: Tailwind CSS v4 via `package.json` in `src/PlaylistFlowbench` (`tailwindcss` + `@tailwindcss/cli`). Source is `Styles/app.css` (`@theme` tokens → `bg-deep/bg-surface/bg-card/text-ivory/...`); generated output is `wwwroot/app.css` — never edit it by hand, rebuild instead.
- Solution uses new `.slnx` format (`playlist-flowbench.slnx`), not `.sln`. Build/run via `dotnet` CLI; `bin/`/`obj/` are gitignored build output, never edit.
- Icons: Lucide via `BlazorBlueprint.Icons.Lucide` (`<LucideIcon Name="library" Size="16" StrokeWidth="1.5" />`, kebab-case names from lucide.dev, inherits `currentColor`). Namespace is in `Components/_Imports.razor` — do not hand-write SVGs when a Lucide name exists.

## Commands
- Run dev server: `dotnet run --project src/PlaylistFlowbench` → http `http://localhost:5019`, https `https://localhost:7095` (see `Properties/launchSettings.json`).
- Rebuild CSS after changing classes/tokens (from `src/PlaylistFlowbench`): `npm run css` (one-shot) or `npm run css:watch` (dev).
- Build: `dotnet build playlist-flowbench.slnx`
- No tests, lint, formatter, or CI configured yet. Do not invent test commands; flag when adding them as a new dependency (needs user approval).

## Entrypoints
- `src/PlaylistFlowbench/Program.cs` → `Components/App.razor` → `Components/Routes.razor` → `Components/Pages/` + `Components/Layout/`.
- Routes: `/` Library, `/playlists/{slug}` Overview. Pages read data via `IPlaylistRepository` (mock in `Services/`), never static data or markup-hardcoded content.
- Static assets: `wwwroot/app.css` + scoped `*.razor.css`.

## Sources of truth
- Product/domain spec: `docs/context.md` (42 sections). Trust it over guesswork.
- `docs/design-prompt.md` is the legacy Google Stitch prompt — do not treat as current spec.
- Canonical visual prototype lives in **Figma** (connected via Composio `figma` toolkit + Figma MCP). Pull component/layout detail from Figma before inventing UI.

## Domain constraints (from `docs/context.md`, easy to violate)
- Platform-agnostic normalized `Track` model: `External source → adapter → Normalized Track → Playlist → editor`. Never put Spotify/service-specific concepts in the editor.
- Sequence is **linear** (`01 → 02 → 03`), not an arbitrary graph — even though canvas looks node-like (DaVinci Resolve inspiration).
- Transitions: **observations, not verdicts**. Show `+13 BPM`, never `Bad transition` / scores.
- `Bpm`/`Key`/`Energy` are nullable (integrations may omit them); energy is user-overridable.
- Future backend: modular monolith (Blazor + ASP.NET Core + relational DB), not microservices. Presentation → Application → Domain → Infrastructure.

## Design tokens (don't invent new ones)
- Palette + typography live in `docs/context.md` §23–24: dark plum surfaces (`#352F44/#26212F`), warm ivory (`#FAF0E6`), muted accents only; predominantly monospace (IBM Plex / JetBrains / Geist Mono).
- Glassmorphism only for floating overlays (inspector, toolbar, menus) — track cards stay solid.
- Desktop-first, primary target `1440×1024`.

## Git Commits

After completing every task, recommend a Conventional Commit message based on the changes implemented.

Format:
`<type>(<scope>): <description>`

Examples:
- `feat(editor): add draggable track cards`
- `feat(navigation): add application navigation`
- `refactor(ui): migrate styles to tailwind`
- `fix(editor): correct track reordering behavior`

Do not create or execute the commit unless explicitly requested. Only provide the recommended commit message at the end of the response.
