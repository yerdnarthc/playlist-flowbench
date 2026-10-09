# Playlist Flowbench — initial domain class diagram

Database planning baseline · 9 October 2026

Based on `docs/context.md` §§11–14 and §§31–38 and the existing Blazor prototype. This is a UML class model for planning persistence. **Core classes below are implemented in this change; the expansion diagram is planned work.** Existing `Models/PlaylistSummary.cs` and `Models/PlaylistOverview.cs` remain presentation read models.

## 1. Implemented core

`?` means nullable; `Guid` identifies entities; timestamps use UTC `DateTimeOffset`. Multiplicities describe the intended persisted relationships. Nullable navigation properties permit objects to be loaded without their related objects; they do not make required foreign-key relationships optional. Collection navigation properties are represented by association lines.

```mermaid
classDiagram
    direction TB
    class User {
        +Guid Id
        +string DisplayName
        +DateTimeOffset CreatedAt
    }
    class Playlist {
        +Guid Id
        +Guid UserId
        +string Name
        +string? Description
        +string? SourceType
        +string? ExternalId
        +DateTimeOffset CreatedAt
        +DateTimeOffset UpdatedAt
    }
    class PlaylistTrack {
        +Guid Id
        +Guid PlaylistId
        +Guid TrackId
        +int Position
        +Guid? SectionId
        +bool IsLocked
        +double? UserEnergyOverride
        +DateTimeOffset CreatedAt
        +DateTimeOffset UpdatedAt
    }
    class Track {
        +Guid Id
        +string? ExternalSource
        +string? ExternalId
        +string Title
        +string Artist
        +string? Album
        +TimeSpan Duration
        +string? ArtworkUrl
        +decimal? Bpm
        +string? MusicalKey
        +double? Energy
        +DateTimeOffset CreatedAt
        +DateTimeOffset UpdatedAt
    }
    class Section {
        +Guid Id
        +Guid PlaylistId
        +string Name
        +string? Notes
        +string? Color
        +DateTimeOffset CreatedAt
        +DateTimeOffset UpdatedAt
    }
    User "1" --> "0..*" Playlist : owns
    Playlist "1" *-- "0..*" PlaylistTrack : orders
    Track "1" <-- "0..*" PlaylistTrack : references
    Playlist "1" *-- "0..*" Section : groups
    Section "0..1" <-- "0..*" PlaylistTrack : assigned to
```

### Why these classes come first

- **User:** an ownership profile, with no password or token fields. Integrate with ASP.NET Core Identity later, explicitly mapping the authenticated identity to the domain owner.
- **Playlist:** editable arrangement metadata and ownership.
- **Track:** normalized song metadata, independent of music-service adapters.
- **PlaylistTrack:** a specific occurrence of a song. A playlist may repeat the same `TrackId`; each occurrence has its own ID and position.
- **Section:** a named contiguous run of occurrences. Membership and playback order determine its boundaries.

These are initial POCO models. They do not yet enforce validation, ownership, locking, ordering, or navigation consistency. Application/domain operations and database mappings must implement the rules below before accepting persisted edits.

## 2. Planned versioning and transition classes

The core `Playlist`, `User`, and `Track` boxes are references to the implemented classes above. All other classes in this diagram are **planned**, not included in the initial model commit.

```mermaid
classDiagram
    direction TB
    class PlaylistVersion {
        +Guid Id
        +Guid PlaylistId
        +Guid CreatedBy
        +Guid? ParentVersionId
        +int VersionNumber
        +string Name
        +string? Description
        +bool IsFinal
        +DateTimeOffset CreatedAt
    }
    class PlaylistVersionTrack {
        +Guid Id
        +Guid PlaylistVersionId
        +Guid TrackId
        +Guid OriginalPlaylistTrackId
        +Guid? VersionSectionId
        +int Position
        +bool IsLocked
        +double? UserEnergyOverride
        +string TitleSnapshot
        +string ArtistSnapshot
        +TimeSpan DurationSnapshot
        +decimal? BpmSnapshot
        +string? MusicalKeySnapshot
        +double? EnergySnapshot
    }
    class PlaylistVersionSection {
        +Guid Id
        +Guid PlaylistVersionId
        +string Name
        +string? Notes
        +string? Color
    }
    class TransitionNote {
        +Guid Id
        +Guid PlaylistVersionId
        +Guid FromVersionTrackId
        +Guid ToVersionTrackId
        +string? Note
        +bool IsIntentional
        +bool IsLocked
        +DateTimeOffset CreatedAt
        +DateTimeOffset UpdatedAt
    }
    Playlist "1" *-- "0..*" PlaylistVersion : versions
    User "1" --> "0..*" PlaylistVersion : authors
    PlaylistVersion "0..1" <-- "0..*" PlaylistVersion : parent
    PlaylistVersion "1" *-- "0..*" PlaylistVersionTrack : snapshots
    Track "1" <-- "0..*" PlaylistVersionTrack : references
    PlaylistVersion "1" *-- "0..*" PlaylistVersionSection : snapshots
    PlaylistVersionSection "0..1" <-- "0..*" PlaylistVersionTrack : assigned to
    PlaylistVersion "1" *-- "0..*" TransitionNote : contains
    PlaylistVersionTrack "1" <-- "0..*" TransitionNote : from occurrence
    PlaylistVersionTrack "1" <-- "0..*" TransitionNote : to occurrence
```

Versioning decisions to implement together:

- Add nullable `Playlist.CurrentVersionId` when the version model lands; it must refer to a version of that same playlist. It identifies the selected saved baseline. The core playlist occurrences hold working edits.
- `PlaylistVersionSection` extends the conceptual specification so saved sections cannot change when working sections are renamed or deleted.
- Transition endpoints refer to **version occurrence IDs**, refining the spec's ambiguous `FromTrackId` / `ToTrackId`. This distinguishes repeated songs. Endpoints must belong to the same version and be adjacent in its linear order.
- Working transition edits can live in editor state and be persisted into a draft version on save/autosave. A future draft-persistence operation must atomically snapshot tracks, sections, and transition notes.
- `OriginalPlaylistTrackId` is a provenance value, **not a foreign key** to a mutable occurrence that may later be deleted. It supports comparison across versions.
- Snapshot fields preserve historical display/analysis values when shared metadata changes. Track references must not cascade-delete history; prefer archiving shared tracks.
- Finalized snapshots are immutable. Editing a final version creates a new draft with `ParentVersionId`; parent and child must share the same playlist.
- BPM and energy deltas are calculated from adjacent occurrences. They are observations and are not stored as quality scores.

## 3. Planned integration and tagging classes

All classes except `User` and `Playlist` below are planned.

```mermaid
classDiagram
    direction TB
    class IntegrationConnection {
        +Guid Id
        +Guid UserId
        +string Provider
        +string ExternalUserId
        +string? CredentialReference
        +DateTimeOffset? ExpiresAt
        +DateTimeOffset CreatedAt
    }
    class Tag {
        +Guid Id
        +Guid UserId
        +string Name
    }
    class PlaylistTag {
        +Guid PlaylistId
        +Guid TagId
    }
    User "1" *-- "0..*" IntegrationConnection : connects
    User "1" *-- "0..*" Tag : defines
    Playlist "1" *-- "0..*" PlaylistTag : labels
    Tag "1" <-- "0..*" PlaylistTag : references
```

`CredentialReference` points to protected server-side credential storage; it is not a plaintext OAuth token. Tags are user-scoped. A playlist can only use its owner's tags. Music providers remain behind adapters; source identifiers are metadata, not editor behavior.

## 4. Database design checklist

| Area | Intended constraint or mapping |
| --- | --- |
| IDs | Entity `Id` is the primary key. `PlaylistTag` later uses `(PlaylistId, TagId)` as its composite primary key. |
| Ownership | `Playlist.UserId` is required. Authorize every operation against the authenticated owner. |
| Sequence | Unique `(PlaylistId, Position)`, positions start at 1 and are contiguous. Reordering must be transactional and avoid temporary uniqueness conflicts. Never enforce unique `(PlaylistId, TrackId)`. |
| Sections | A nullable section association allows ungrouped tracks. Enforce that the section and occurrence share a playlist, preferably with a composite foreign key `(SectionId, PlaylistId)` to `(Id, PlaylistId)`. Section membership must form a contiguous run. |
| Metadata | Nonblank names/titles/artists; nonnegative duration; positive BPM when supplied; energy and overrides in `[0, 1]`. Unknown BPM, key, and energy stay null, never zero placeholders. |
| Energy | Effective energy is `UserEnergyOverride ?? Track.Energy`; calculate it after loading metadata. Do not duplicate it in a persisted column. |
| Sources | Source and external ID should be either both provided or both absent. Select provider-specific identity/uniqueness rules when adapters are implemented; manual tracks need no external ID. |
| Numeric storage | Map duration to integer ticks or milliseconds explicitly; choose precision for BPM. Avoid provider-dependent SQL time-of-day mappings for elapsed duration. |
| Deletion | Playlist deletion removes its occurrences/sections; shared tracks survive. Section removal unassigns occurrences in a transaction. Restrict referenced track deletion; preserve version history. Configure cascade paths explicitly. |
| Locking | Reorder/delete operations must respect `IsLocked`; setters alone do not enforce it. Define transition-lock behavior alongside version operations. |
| Versions | Unique `(PlaylistId, VersionNumber)` and `(PlaylistVersionId, Position)`; validate same-version section/endpoints and same-playlist parent/current-version references. |
| Transition notes | At most one note per `(PlaylistVersionId, FromVersionTrackId, ToVersionTrackId)`. Invalidate or explicitly remap notes when draft adjacency changes. |
| Timestamps/concurrency | Update `UpdatedAt` in write operations. Add a database-appropriate optimistic-concurrency token before enabling persistence/autosave. |
| Derived UI | Track counts, formatted durations, BPM ranges, energy curves, and relative edited labels belong in projections. Canvas coordinates/collapse state are future UI state, not playback order. |

## 5. Implementation path

1. **This change:** diagram and core `User`, `Playlist`, `Track`, `PlaylistTrack`, and `Section` classes in `src/PlaylistFlowbench/Domain/`.
2. Choose a relational database and add EF Core mappings, validation/ordering operations, ownership integration, and migrations. Add focused tests for domain rules at that stage.
3. Implement draft/version snapshots and transition persistence together, with atomic save and concurrency handling.
4. Map domain data into the existing presentation read models and replace `MockPlaylistRepository` with the persistence implementation.
5. Add provider adapters, protected credentials, and tagging as their features are developed.

No database provider, migration, authentication implementation, or new package is introduced by this initial change.
