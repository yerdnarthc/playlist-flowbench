# Playlist Flowbench — Project Website Application Context

## 1. Product Definition

**Product name:** Playlist Flowbench  
**Product type:** Web application / playlist sequencing workbench  
**Primary stack:** C# + ASP.NET Core + Blazor  
**Prototype stage:** Figma-first; no production implementation is required yet  
**Primary interaction:** Visual drag-and-drop sequencing of tracks  
**Product philosophy:** Give the user better tools for arranging music without taking creative control away from them.

### Core proposition

Playlist Flowbench is a **visual sequencing workstation for playlists**.

Music platforms are generally optimized around discovering, collecting, and playing tracks. Flowbench focuses on a narrower problem:

> **How do I make the order of songs in a playlist feel intentional?**

The application imports or receives a playlist, exposes useful track metadata, presents the playlist as a visual sequence of cards, and helps the user inspect relationships between adjacent tracks.

The application is **not** intended to automatically generate a "perfect playlist." Its role is to make the user's own sequencing decisions easier, faster, and more informed.

A useful mental model is:

> **A DAW-like editing workbench for playlist order.**

The product is platform-agnostic. Spotify may be one integration, but the core domain must not depend on Spotify. Future adapters may support other music services and local/imported track metadata.

---

# 2. Problem Statement

Playlist creation is easy; playlist refinement is tedious.

A user may build a playlist gradually over weeks or months. Once it becomes large, several annoyances appear:

- A song transition feels awkward, but the reason is not immediately obvious.
- BPM changes, keys, energy, duration, and other metadata are difficult to compare while manually rearranging tracks.
- Reordering dozens of songs in a conventional list is cognitively expensive.
- The user may want deliberate phases such as intro, build, peak, and cooldown.
- A playlist can have multiple possible arrangements and users may want to compare versions.
- Some transitions are intentionally abrupt; therefore the software must not treat every discontinuity as an error.
- Native music-platform playlist editors typically prioritize collection management and playback rather than deep sequencing workflows.

Flowbench addresses these annoyances with a visual editing environment.

---

# 3. Product Goals

## Primary goals

1. Make playlist sequencing spatial, visual, and easy to manipulate.
2. Make adjacent-track relationships easy to inspect.
3. Preserve user authorship and creative intent.
4. Support deliberate playlist structure through sections and grouping.
5. Make experimentation safe through drafts and version comparison.
6. Provide useful information without overwhelming the user.
7. Build a product architecture that can support multiple music-service integrations later.

## Secondary goals

- Provide quick playlist health/cleanup observations.
- Allow users to record transition notes and sequencing rationale.
- Provide optional, non-authoritative flow observations.
- Produce a clean, professional desktop-first interface suitable for keyboard-and-mouse workflows.

---

# 4. Non-Goals

Flowbench should deliberately avoid becoming:

- A full music streaming service.
- A social network for playlist sharing.
- A generic music discovery engine.
- A recommendation platform.
- A DAW or audio editor.
- A complete DJ performance system.
- A generalized project-management platform.
- An AI service whose primary value proposition is automatic playlist generation.

The product succeeds by being a **focused sequencing tool**, not by accumulating unrelated features.

---

# 5. Target Users

## Primary persona — Playlist Curator

A person who creates long-form playlists for specific moods, activities, environments, or narratives.

Examples:

- Late-night driving playlist
- Workout progression
- Study/coding playlist
- Conceptual or thematic listening experience
- Genre progression
- Personal "album-like" playlists

They care about ordering, transitions, pacing, and intentionality.

## Secondary persona — Music Enthusiast / Amateur DJ

A user who already thinks in BPM, keys, energy, and transitions but does not necessarily need or want a full DJ application.

## Tertiary persona — Creative Curator

A filmmaker, editor, game developer, sound designer, or content creator who creates references/playlists and wants structured sequencing rather than simple sorting.

---

# 6. Core User Experience

The main flow should feel like this:

```text
Open Flowbench
    ↓
Choose / import playlist
    ↓
Playlist overview
    ↓
Open Flowbench editor
    ↓
Arrange track cards
    ↓
Inspect transitions
    ↓
Create / adjust sections
    ↓
Save draft
    ↓
Compare versions if needed
    ↓
Export / sync arrangement
```

The central experience is the **Flowbench Editor**.

---

# 7. Core Features

## 7.1 Playlist Import

Users should be able to bring a playlist into Flowbench.

For the prototype, this can be represented as mocked data rather than real API integration.

Supported conceptual sources:

- Spotify
- Apple Music
- YouTube Music
- Tidal
- Local/imported metadata
- CSV/JSON/manual import in later development

### Important architectural principle

The domain model should use Flowbench's own normalized track model:

```text
External source
    ↓
Integration adapter
    ↓
Normalized Track
    ↓
Playlist
    ↓
Flowbench editor
```

The editor must not contain Spotify-specific concepts.

---

## 7.2 Playlist Overview

Before entering the editor, show an overview of the playlist.

Useful information:

- Playlist title
- Description
- Track count
- Total duration
- Number of sections
- Last edited timestamp
- Draft/version state
- Source platform
- Optional high-level flow visualization
- Recently modified tracks

Avoid turning this into a KPI-heavy business dashboard.

This is a creative tool, so the overview should feel like a **workspace**, not an analytics SaaS.

---

## 7.3 Flowbench Editor

This is the heart of the application.

The user sees tracks as **large movable cards** positioned on a free-form editing canvas.

Conceptually:

```text
[01 Track A] ─── [02 Track B] ─── [03 Track C] ─── [04 Track D]
```

Cards can be:

- dragged
- reordered
- selected
- multi-selected
- locked
- duplicated in a draft
- inspected
- grouped into sections

The visual language should take inspiration from node-based professional tools such as DaVinci Resolve's node workspace, but the information architecture is **card-based rather than literal node graphs**.

### Important behavior

The sequence remains fundamentally linear.

The canvas may use node-like spatial presentation, but the user should always understand:

> Track 01 → Track 02 → Track 03 → Track 04

Avoid turning the product into a complex arbitrary graph editor.

---

# 8. Track Card Model

Each card should present information at a glance.

Suggested content:

```text
Track number
Album artwork / visual
Track title
Artist
Duration
BPM
Musical key
Energy indicator
Lock state
```

Optional metadata can be hidden until expanded.

### Example

```text
┌──────────────────────────────┐
│ 04                       🔒 │
│                              │
│ ┌──────┐                     │
│ │ ART  │  Midnight City      │
│ │ WORK │  M83                │
│ └──────┘                     │
│                              │
│ 105 BPM   F♯ minor   04:03  │
│ ────────────────            │
│ Energy  ▂▃▅▆                 │
└──────────────────────────────┘
```

The card should be readable without feeling like a database row.

---

# 9. Transition Inspection

The relationship between adjacent tracks is a first-class interaction.

Clicking the connector/transition between two cards opens a contextual inspector.

Example:

```text
TRACK A
112 BPM
C major

        ↓

TRACK B
117 BPM
G major
```

The inspector can show:

- BPM delta
- key relationship
- energy delta
- duration
- user note
- transition status
- lock state
- optional "intentional jump" flag

### Design philosophy

Flowbench should provide **observations, not verdicts**.

Good:

> Tempo change: +22 BPM

Less desirable:

> Bad transition

Good:

> Large energy increase

Less desirable:

> Playlist quality: 62/100

There must be no requirement that smoothness is always preferable.

An abrupt transition may be intentional and creatively correct.

---

# 10. Energy Curve

A playlist can optionally expose a compact visual energy curve.

Example:

```text
▂ ▃ ▃ ▅ ▆ █ █ ▇ ▅ ▃ ▂
```

The curve can help the user see broad pacing.

The curve should be treated as **context**, not as an authoritative metric.

Potential inputs:

- Imported metadata
- User-adjusted energy values
- Future derived metadata

The user should be able to override an energy value manually.

---

# 11. Sections

Users can group a sequence into sections.

Examples:

```text
INTRO
BUILD
CRUISE
PEAK
COOLDOWN
OUTRO
```

But sections are completely user-defined.

A playlist may instead use:

```text
DEPARTURE
NEON
MIDNIGHT
HOME
```

Sections should behave like lightweight containers around a run of cards.

The section itself may expose:

- name
- track count
- duration
- collapsed/expanded state
- color/accent marker
- notes

Avoid turning sections into heavy project-management objects.

---

# 12. Locking and Constraints

Users may decide that a track or transition must remain fixed.

Examples:

```text
🔒 Track A
🔒 Track B
```

or:

```text
🔒 A → B
```

Locked items help users experiment without accidentally disturbing deliberate sequencing decisions.

Possible future constraints:

- Keep together
- Keep within section
- Preserve position
- Preserve transition

---

# 13. Drafts and Versioning

Sequencing is iterative.

Users should be able to create versions such as:

```text
Draft 01
Draft 02
Final
```

Version metadata:

- version number
- name
- created timestamp
- author
- optional note
- parent version

The system should treat versions as immutable snapshots once finalized.

---

# 14. Version Comparison

Allow two arrangements to be compared.

Example:

```text
VERSION A               VERSION B

01 Track A              01 Track A
02 Track B              02 Track C   ← moved
03 Track C              03 Track B   ← moved
04 Track D              04 Track D
```

The comparison should emphasize **structural differences**, not produce a numerical winner.

---

# 15. Playlist Health / Cleanup

This is optional and should remain secondary to sequencing.

Potential observations:

```text
13 duplicate tracks
2 unavailable tracks
3 repeated artists in close succession
1 unusually long gap
```

These should be presented as review items.

Avoid aggressive warning badges everywhere.

---

# 16. Search, Filtering, and Sorting

The application should support fast local manipulation.

Search by:

- track title
- artist
- album
- section
- tags

Filter by:

- BPM range
- key
- energy
- duration
- section
- locked/unlocked

Sorting should be reversible and clearly indicate when sorting would alter the sequence versus merely changing the view.

---

# 17. Interaction Requirements

Important interactions for the prototype:

### Drag and drop

Drag track cards to reorder.

### Multi-select

Select multiple cards to move or group.

### Context menu

Right-click or use a card menu for:

- Inspect
- Lock
- Move to section
- Duplicate
- Remove
- Add note

### Keyboard shortcuts

Potential shortcuts:

```text
Ctrl/Cmd + Z        Undo
Ctrl/Cmd + Shift + Z Redo
Ctrl/Cmd + S        Save
Delete              Remove
Space               Play/pause preview
Arrow keys          Fine movement
F                   Fit canvas
```

Exact implementation can be decided later; the prototype should make shortcut discoverability possible.

---

# 18. Application Information Architecture

Suggested top-level structure:

```text
Flowbench
│
├── Home
│
├── Playlists
│   ├── All playlists
│   ├── Favorites
│   └── Recently edited
│
├── Flowbench Editor
│
├── Versions
│
├── Imports / Connections
│
└── Settings
```

Avoid a huge sidebar with dozens of destinations.

---

# 19. Suggested Screens for the Figma Prototype

The prototype should cover these screens.

## Screen 1 — Landing / Welcome

Purpose:

Explain the product quickly.

Content:

- Flowbench logo
- short value proposition
- "Open a playlist"
- "Create from import"
- recent playlists

## Screen 2 — Playlist Library

A workspace for user's playlists.

Cards should contain:

- title
- track count
- duration
- last edited
- section count
- small flow preview

## Screen 3 — Playlist Overview

Provides context before editing.

## Screen 4 — Main Flowbench Editor

Most important screen.

Contains:

- app navigation
- top editor toolbar
- canvas
- track cards
- connectors
- section containers
- right inspector panel
- zoom controls
- optional minimap

## Screen 5 — Transition Inspector

Detailed contextual view for an adjacent-track relationship.

## Screen 6 — Section Inspector

Edit section title, notes, and metadata.

## Screen 7 — Version Compare

A/B arrangement comparison.

## Screen 8 — Import / Connect Source

For design purposes, represent external music sources without implementing them.

## Screen 9 — Settings

Minimal:

- account
- appearance
- integrations
- editor preferences
- shortcuts
- data/privacy

---

# 20. Main Editor Layout

A recommended desktop composition:

```text
┌──────────────────────────────────────────────────────────────┐
│ Top toolbar                                                  │
├────────┬───────────────────────────────────────┬─────────────┤
│        │                                       │             │
│ Side   │             FLOW CANVAS              │ Inspector   │
│ rail   │                                       │             │
│        │ [A] → [B] → [C] → [D]                │ Transition  │
│        │                                       │ Details     │
│        │ [E] → [F] → [G]                     │             │
│        │                                       │             │
│        │                                       │             │
├────────┴───────────────────────────────────────┴─────────────┤
│ Status / zoom / canvas controls                              │
└──────────────────────────────────────────────────────────────┘
```

### Approximate proportions

- Navigation rail: 64–84 px
- Main canvas: ~65–75% of usable area
- Inspector: ~280–360 px
- Top toolbar: ~56–64 px

Exact values can be refined in Figma.

---

# 21. UX Principles

## User remains in control

Do not aggressively automate sequencing.

## Reveal complexity progressively

Show core information first; expose advanced metadata on selection or expansion.

## Spatial understanding over table-heavy UI

The editor should communicate order through position and visual continuity.

## Fast manipulation

Common actions should require minimal clicks.

## Reversible actions

Undo/redo should feel safe and immediate.

## Context preservation

When a user inspects something, the surrounding sequence should remain visible.

## Respect intentional imperfection

Not every BPM jump or key change is a problem.

---

# 22. Visual Design Direction

The visual system combines:

1. **Minimalism**
2. **Restrained glassmorphism**
3. **Utilitarian/professional tooling**
4. **Node-editor inspiration**
5. **Technical/editorial visual language**

The result should feel like a **professional creative workstation**, not a generic startup dashboard.

Primary visual reference:

- DaVinci Resolve node/editor interfaces
- Dense but deliberate professional-tool layouts

Secondary references supplied for the project:

- expressive editorial card composition
- technical/retro-futurist typography and layouts
- muted palette studies
- modern productivity dashboard structure

These references should inform **composition, hierarchy, density, and material treatment**, not be copied literally.

---

# 23. Color System

Primary user-provided palette:

```text
#352F44  Deep Plum / Charcoal
#5C5470  Muted Violet Slate
#B9B4C7  Lavender Gray
#FAF0E6  Warm Ivory
```

Suggested supporting colors:

```text
#26212F  Near-black Plum
#2E2839  Deep Surface
#423B50  Elevated Surface
#70687F  Muted Text / Divider
#D8D3DC  Light Text / Light Surface
#F2E8DD  Warm Secondary Surface

#8C789C  Muted Plum Accent
#B47D5F  Muted Copper Accent
#738B7B  Muted Sage Accent
#A36F6F  Dusty Brick Accent
#B39A65  Muted Ochre Accent
```

### Palette philosophy

The UI should feel mostly monochromatic with occasional restrained accents.

Avoid:

- saturated rainbow dashboards
- excessive purple-blue gradients
- neon SaaS aesthetics
- generic AI product palettes
- ubiquitous red/yellow/green pill badges

Semantic state should preferably be conveyed through:

- iconography
- shape
- typography
- subtle border treatments
- position
- line weight
- muted tonal changes

rather than relying on loud status colors.

---

# 24. Typography

Typography should be predominantly monospace.

Recommended direction:

- IBM Plex Mono
- JetBrains Mono
- Geist Mono
- another high-quality contemporary monospace family

Use weight contrast aggressively but intentionally:

```text
DISPLAY / PAGE TITLE
Bold / ExtraBold

SECTION
Bold / Semibold

LABEL
Medium / Semibold

BODY
Regular

METADATA
Regular / Medium
```

Long-form paragraphs can optionally use a complementary sans-serif if readability requires it, but the overall visual identity should remain strongly monospace.

Typography should feel:

- technical
- precise
- engineered
- editorial
- slightly experimental

Avoid:

- bubbly rounded startup typography
- oversized marketing typography
- excessive all-caps
- decorative sci-fi fonts

---

# 25. Shape Language

Use:

- medium corner radii
- controlled rounding
- subtle borders
- stacked surfaces
- compact controls
- clear alignment
- deliberate spacing

Avoid:

- excessive pill shapes
- floating cards everywhere
- giant rounded containers
- overly soft "mobile app" UI

Cards should feel like **pieces of an instrument panel**, not generic SaaS feature cards.

---

# 26. Glassmorphism Guidelines

Glassmorphism is a material treatment, not the entire interface.

Use it selectively:

- floating inspector panels
- toolbar overlays
- transient menus
- dialogs
- contextual controls

Recommended characteristics:

- moderate transparency
- backdrop blur
- thin low-contrast border
- subtle highlight
- low shadow
- underlying canvas remains visible

Avoid making every card transparent.

Track cards should generally remain visually solid enough to preserve readability and hierarchy.

---

# 27. Canvas / Node-Editor Aesthetic

The editor canvas is the signature.

Use:

- dark canvas
- subtle grid/dot pattern
- slightly elevated cards
- thin connectors
- restrained accent highlights
- zoomable workspace
- minimap
- alignment guides
- snap indicators

Reference the feel of professional node tools such as DaVinci Resolve without cloning its exact UI.

The interface should communicate:

> **technical tool + creative instrument**

---

# 28. Component System

Design reusable components in Figma.

Suggested components:

```text
AppShell
NavigationRail
TopToolbar
TrackCard
TrackCard/Selected
TrackCard/Locked
TrackCard/Compact
TransitionConnector
TransitionInspector
SectionContainer
SectionHeader
MetadataChip
SearchField
FilterControl
ContextMenu
Toast
Modal
Tooltip
VersionCard
VersionCompareRow
ZoomControl
Minimap
CanvasToolbar
```

Create variants for:

- default
- hover
- selected
- focused
- disabled
- locked
- dragging
- drop target

---

# 29. Accessibility

Even for the prototype, design for:

- keyboard navigation
- visible focus states
- sufficient contrast
- non-color-only meaning
- readable metadata
- predictable control placement
- reasonable target sizes
- reduced-motion consideration

The canvas should have a keyboard-accessible alternative for reordering so the product does not depend exclusively on drag-and-drop.

---

# 30. Responsive Strategy

The first prototype should be **desktop-first** because sequencing is a precision-heavy workspace.

Primary target:

- 1440 × 1024 desktop/laptop

Secondary:

- 1280 × 800

Tablet/mobile should not simply shrink the desktop editor.

A future responsive mode may switch to:

```text
Canvas
↓
Track stack
↓
Inspector drawer
```

But mobile is not the primary editing experience.

---

# 31. Data Model — Conceptual

Core entities:

```text
User
Playlist
Track
PlaylistTrack
Section
TransitionNote
PlaylistVersion
PlaylistVersionTrack
IntegrationConnection
Tag
PlaylistTag
```

### Playlist

```text
Id
UserId
Name
Description
SourceType
ExternalId
CreatedAt
UpdatedAt
CurrentVersionId
```

### Track

```text
Id
ExternalSource
ExternalId
Title
Artist
Album
Duration
ArtworkUrl
Bpm
Key
Energy
CreatedAt
UpdatedAt
```

BPM, key, and energy should be nullable because not all integrations or imports will provide them.

### PlaylistTrack

```text
Id
PlaylistId
TrackId
Position
SectionId
IsLocked
UserEnergyOverride
CreatedAt
UpdatedAt
```

### TransitionNote

```text
Id
PlaylistVersionId
FromTrackId
ToTrackId
Note
IsIntentional
CreatedAt
UpdatedAt
```

### PlaylistVersion

```text
Id
PlaylistId
Name
Description
VersionNumber
CreatedAt
CreatedBy
IsFinal
```

---

# 32. Architecture Direction

The eventual production system should favor modularity without prematurely creating microservices.

Recommended conceptual layers:

```text
Presentation
    ↓
Application
    ↓
Domain
    ↓
Infrastructure
```

## Presentation

Blazor components, pages, state containers, UX interaction.

## Application

Use cases:

- ImportPlaylist
- RearrangeTracks
- CreateSection
- InspectTransition
- SaveDraft
- CreateVersion
- CompareVersions
- ExportPlaylist

## Domain

Pure concepts and business rules:

- Playlist
- Track
- Section
- Transition
- Version
- Sequencing constraints

## Infrastructure

- Entity Framework Core
- database
- external music APIs
- storage
- authentication
- logging

Avoid coupling domain logic to external platform APIs.

---

# 33. Security Requirements

Even though this is only a prototype now, the architecture should be designed for safe production expansion.

## Authentication

Use established ASP.NET Core authentication rather than homegrown authentication.

## External integrations

Use OAuth-style authorization where supported.

Never store a user's music-service password.

## Tokens

External refresh/access tokens should be:

- encrypted at rest
- scoped minimally
- revocable
- excluded from logs

## Authorization

Every playlist should be owned by a user and access-controlled server-side.

Never rely solely on UI hiding for authorization.

## Input validation

Validate:

- imported metadata
- playlist names
- notes
- external IDs
- file uploads

## File handling

For future local import support:

- validate file type
- limit size
- sanitize filenames
- never execute uploaded content
- isolate media processing

## Privacy

Only collect data necessary for the product.

---

# 34. Performance Requirements

The editing experience should feel immediate.

Important principles:

- local UI state should update optimistically where safe
- drag/reorder interactions should not require full-page reloads
- large playlists should use virtualization or progressive rendering
- metadata calculations should be cached
- autosave should be debounced
- expensive imports should run asynchronously

Potential target:

> A user moving a track should see the visual result essentially immediately, independent of network latency.

---

# 35. Scalability Direction

The initial implementation can be a modular monolith.

Do not start with microservices.

A reasonable progression:

```text
Phase 1
Blazor + ASP.NET Core + relational DB

Phase 2
Background processing + caching

Phase 3
Object storage / job processing if imports become heavy

Phase 4
Service extraction only where real load justifies it
```

The architecture should preserve clean module boundaries while keeping deployment simple.

---

# 36. Reliability

Use:

- transactional persistence for version creation
- optimistic concurrency for collaborative/future-proof editing
- autosave
- undo/redo state
- version snapshots
- activity timestamps
- graceful handling of failed external integrations

A user's arrangement should never silently disappear because an API call failed.

---

# 37. Observability

Production architecture should allow:

- structured logging
- error tracking
- performance telemetry
- integration failure monitoring
- audit information for important operations

Avoid logging sensitive OAuth tokens or private user content unnecessarily.

---

# 38. Testing Strategy

### Unit tests

Test domain behavior:

- valid/invalid reorder operations
- section boundaries
- locking behavior
- version creation
- transition calculations

### Integration tests

Test:

- database persistence
- authentication
- external integration adapters
- import/export

### Component/UI tests

Test:

- drag/drop state
- card selection
- inspector behavior
- keyboard interactions

### End-to-end

Test the main journey:

```text
Import playlist
→ Open editor
→ Move track
→ Save
→ Create version
→ Compare
```

---

# 39. Future Features

Potential later expansion:

- Spotify integration
- Apple Music integration
- YouTube Music integration
- Tidal integration
- local CSV/JSON import
- smart metadata enrichment
- playlist cleanup
- advanced key/tempo analysis
- waveform or preview visualization where permitted
- public read-only playlist pages
- collaboration
- comments
- version history
- playlist sharing
- export back to supported services
- keyboard-first workflow
- command palette

These should only be added when the core sequencing experience is strong.

---

# 40. Product Success Criteria

A successful Flowbench prototype should communicate all of the following within seconds:

1. This is for arranging playlists.
2. Songs are movable cards.
3. The sequence is visually connected.
4. Track relationships can be inspected.
5. Sections create larger narrative structure.
6. The tool is professional and precise.
7. The user is still the creative decision-maker.

If a viewer thinks:

> "Oh, this is basically a music editor for playlist order."

the design has succeeded.

---

# 41. Prototype Acceptance Criteria

The Figma prototype should allow a viewer to conceptually experience:

```text
Landing
→ choose playlist
→ open playlist
→ enter Flowbench
→ drag a track
→ observe updated sequence
→ inspect transition
→ create section
→ save draft
→ compare versions
```

The prototype does **not** need real API connections, audio playback, authentication logic, or database behavior.

Use realistic mocked data to make the experience believable.

---

# 42. Design North Star

The application should feel like:

> **A precision instrument for arranging listening experiences.**

Not:

> another playlist app.

Not:

> another generic SaaS dashboard.

Not:

> an AI playlist generator.

It should feel deliberate, technical, tactile, quiet, and powerful.
