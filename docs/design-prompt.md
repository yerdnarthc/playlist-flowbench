# Stitch / Figma Design Prompt — Playlist Flowbench

Design a complete desktop-first website application prototype called **Playlist Flowbench**.

## 1. Product

Playlist Flowbench is a **visual sequencing workbench for playlists**.

The user imports or opens an existing playlist and arranges the songs as movable cards on a spatial canvas. The application exposes useful track metadata—BPM, musical key, energy, duration, artist, album artwork—and allows users to inspect the relationship between adjacent tracks.

The product is **not an AI playlist generator** and must not visually resemble a generic AI SaaS product.

The user remains the creative decision-maker. Flowbench provides information and editing tools; it should never imply that the software knows the objectively "best" playlist order.

Core mental model:

> **A DAW-like professional editing workspace, but for playlist sequencing.**

Primary visual inspiration:

> **DaVinci Resolve's professional node/editor workspace**, especially the dense, deliberate, technical feel of its canvas and node system.

However, do NOT copy DaVinci Resolve's interface literally. Translate the concept into a fresh product language:

- nodes become **cards**
- connections represent playlist order
- sections become spatial containers
- the user can drag and reorder cards
- an inspector shows contextual information
- the workspace uses a zoomable grid canvas

---

# 2. Required Design Language

The entire visual identity should combine:

### Minimalistic Design

Keep the interface restrained and intentional.

Do not fill every area with cards, charts, badges, or decorative widgets.

Every visual element must have a reason to exist.

### Restrained Glassmorphism

Use glass material selectively for:

- floating inspector panels
- toolbar overlays
- menus
- dialogs
- contextual controls

Do NOT make the entire UI transparent.

Cards and important content should remain readable and visually solid.

### Utilitarian Design

The interface should feel like a professional instrument.

Think:

- music production software
- color grading software
- engineering tools
- precision editors
- technical workspaces

Avoid:

- generic startup dashboards
- excessive rounded SaaS cards
- colorful gradient blobs
- overuse of pills
- "AI magic" visual clichés
- excessive soft shadows
- cartoonish onboarding illustrations

---

# 3. Typography

Typography must be predominantly **monospace**.

Use a contemporary high-quality monospace style such as:

- IBM Plex Mono
- JetBrains Mono
- Geist Mono

Prefer monospace for:

- page titles
- section headings
- navigation
- labels
- track metadata
- BPM
- key
- duration
- technical controls
- inspector values
- version identifiers

Use strong weight contrast:

```text
PAGE TITLE     ExtraBold / Bold
SECTION TITLE  Bold
SUBSECTION     Semibold
LABEL          Medium
BODY           Regular
METADATA       Regular / Medium
```

Typography should communicate:

- precision
- technicality
- editorial quality
- confidence
- craft

Avoid sci-fi/decorative typefaces.

Avoid excessive all-caps.

---

# 4. Color System

Base palette:

```text
#352F44
#5C5470
#B9B4C7
#FAF0E6
```

Interpret them as:

```text
#352F44  deepest primary surface / dark plum-charcoal
#5C5470  secondary surface / muted violet slate
#B9B4C7  muted light text / borders / highlights
#FAF0E6  warm ivory light surface
```

Supporting palette:

```text
#26212F  near-black plum
#2E2839  deep interface surface
#423B50  elevated surface
#70687F  muted secondary text
#D8D3DC  light interface text
#F2E8DD  warm secondary surface

#8C789C  muted plum accent
#B47D5F  muted copper accent
#738B7B  muted sage accent
#A36F6F  dusty brick accent
#B39A65  muted ochre accent
```

### Color behavior

The interface should be predominantly dark, using deep plum-charcoal surfaces.

Use #FAF0E6 and #B9B4C7 for strong readable contrast.

Use muted plum/copper/sage/ochre accents sparingly for emphasis.

Do NOT create an "AI-generic" semantic palette.

Avoid:

- neon cyan
- electric blue
- hot pink
- saturated green
- saturated red
- rainbow badges
- excessive purple gradients

Do not turn the UI into a collection of colored status pills.

State can be communicated through:

- icon
- border
- shape
- typography
- small tonal accent
- line treatment

rather than loud colors.

---

# 5. Reference Images

Use the supplied reference images as visual inspiration.

Important visual themes across the supplied references:

### Reference A — DaVinci Resolve

Take inspiration from:

- professional editor density
- dark canvas
- spatial node workspace
- compact controls
- thin connections
- technical UI hierarchy
- powerful but controlled information density

### Reference B — Podcast/dashboard example

Take inspiration from:

- strong card composition
- large visual blocks
- bold type hierarchy
- editorial layouts
- personality without visual clutter

### Reference C — Technical/retro-futurist composition

Take inspiration from:

- technical labeling
- structured panel layouts
- industrial/editorial character
- typography mixed with compact information blocks
- restrained orange/green accents

### Reference D — Color palette/editorial design

Take inspiration from:

- muted, slightly warm retro color relationships
- bold typographic hierarchy
- strong large-scale blocks
- visual contrast without neon colors

### Reference E — Productivity dashboard

Take inspiration from:

- coherent sidebar + main workspace structure
- clear section grouping
- dense but readable information
- reusable cards and controls
- organized hierarchy

Do not copy any reference directly.
Synthesize them into an original product design.

---

# 6. Overall Visual Character

Target adjectives:

**technical**
**minimal**
**editorial**
**professional**
**tactile**
**precise**
**quiet**
**confident**
**slightly retro-futurist**
**creative-tool oriented**

The result should feel like a **professional creative application**, not a generic productivity SaaS site.

---

# 7. Main Application Shell

Design a desktop web application around 1440 × 1024.

Recommended shell:

```text
┌──────────────────────────────────────────────────────────────┐
│ TOP TOOLBAR                                                  │
├────────┬───────────────────────────────────────┬─────────────┤
│ NAV    │                                       │ INSPECTOR   │
│ RAIL   │         FLOWBENCH CANVAS              │             │
│        │                                       │             │
│        │ [CARD] → [CARD] → [CARD] → [CARD]   │             │
│        │                                       │             │
│        │       [CARD] → [CARD]                │             │
│        │                                       │             │
├────────┴───────────────────────────────────────┴─────────────┤
│ CANVAS STATUS / ZOOM / MINIMAP                               │
└──────────────────────────────────────────────────────────────┘
```

Suggested proportions:

- navigation rail: 64–84 px
- inspector: 280–360 px
- editor canvas: largest area
- toolbar: 56–64 px

---

# 8. Navigation Rail

Keep it compact.

Suggested destinations:

```text
Home
Playlists
Versions
Connections
Settings
```

Use simple line icons.

Avoid giant text navigation.

Use icon + label on hover/expanded state if helpful.

Navigation should feel closer to professional desktop software than a consumer mobile app.

---

# 9. Playlist Library Screen

Design a clean library view.

Show playlists as structured visual cards.

Each card:

```text
Playlist artwork / thumbnail
Playlist name
Track count
Total duration
Last edited
Small flow preview
```

Example playlist names:

- Late Night Drive
- Midnight Run
- Neon Rain
- Coding at 2AM
- Quiet Sunday
- After Hours

Use fictional data.

Do not make it feel like Spotify.

The product is Flowbench, not a streaming service.

Primary CTA:

**Open playlist**

Secondary:

**Import playlist**

---

# 10. Playlist Overview Screen

Show:

- title
- description
- source
- duration
- number of tracks
- number of sections
- last edited
- version indicator

Include a restrained energy-curve visualization.

Avoid dashboard-style oversized KPI cards.

This should feel like a **creative project overview**.

Possible page structure:

```text
Playlist title
metadata

Energy curve

Sections
--------------------------------
Intro      4 tracks
Cruise     12 tracks
Peak       8 tracks
Cooldown   6 tracks

Recent changes
--------------------------------
...
```

Primary action:

**Open Flowbench**

---

# 11. MAIN FLOWBENCH EDITOR — MOST IMPORTANT SCREEN

This screen should receive the majority of design effort.

It is the signature visual experience.

## Canvas

Use a deep dark plum canvas.

Subtle grid / dot pattern.

No huge decorative backgrounds.

Cards appear spatially on the canvas.

Example:

```text
[01] ───── [02] ───── [03] ───── [04]


        [05] ───── [06] ───── [07]
```

Prefer a clean linear arrangement with enough breathing room.

The cards can visually sit at slightly different positions, but the sequence must remain obvious.

---

# 12. Track Cards

Cards should feel like **nodes crossed with index cards / studio equipment modules**.

Avoid generic SaaS cards.

Recommended card structure:

```text
┌───────────────────────────────────┐
│ 04                            🔒 │
│                                   │
│ [ART]  Midnight City              │
│        M83                        │
│                                   │
│ 105 BPM     F♯ min     04:03     │
│                                   │
│ Energy ▂▃▅▆                      │
└───────────────────────────────────┘
```

Visual characteristics:

- medium radius
- thin border
- subtle elevation
- dark solid body
- small artwork area
- compact metadata
- bold title
- monospace values

Create variants:

- Default
- Hover
- Selected
- Focused
- Locked
- Dragging
- Drop target
- Compact

Selected cards can use a restrained warm-ivory or muted-plum outline.

Do not use a huge glowing border.

---

# 13. Connectors

Between adjacent cards, use **thin precise connector lines**.

Think DaVinci Resolve node connections, but simpler.

Connectors should visually communicate:

```text
A → B → C → D
```

Clicking a connector should activate the transition inspector.

Possible subtle transition marker:

```text
────────── ◇ ──────────
```

The marker can show that an inspectable relationship exists without screaming for attention.

---

# 14. Transition Inspector

The inspector appears on the right side.

Example:

```text
TRANSITION

MIDNIGHT CITY
M83

        ↓

AFTER DARK
Mr.Kitty

BPM
105 → 118

DELTA
+13 BPM

KEY
F♯ min → A min

ENERGY
medium → high

NOTE
"Nice lift into the chorus."

[ ] Intentional jump

[Lock transition]
```

Keep this panel professional and compact.

Use typography and alignment more than decorative containers.

---

# 15. Energy Curve

Add an unobtrusive miniature energy visualization.

Example:

```text
ENERGY
▂ ▃ ▃ ▅ ▆ █ █ ▇ ▅ ▃ ▂
```

Use line/area visualization rather than chart-heavy dashboard styling.

The energy curve should integrate naturally into the workspace.

---

# 16. Sections

Sections should be visual containers around groups of cards.

Example:

```text
╭─────────────────────────────────────────────╮
│ INTRO                                       │
│                                             │
│ [01] → [02] → [03]                         │
╰─────────────────────────────────────────────╯

╭─────────────────────────────────────────────╮
│ CRUISE                                      │
│                                             │
│ [04] → [05] → [06] → [07]                 │
╰─────────────────────────────────────────────╯
```

Sections should feel subtle and structural.

Do not make them huge colorful boards.

Use:

- label
- thin border
- muted accent line
- track count
- duration

---

# 17. Top Toolbar

Include:

```text
Playlist name
Version
Undo
Redo
Save
Add section
View options
Zoom
Search
```

Potential view controls:

```text
Canvas
Compact
Metrics
```

Do not overload the toolbar.

Use icon buttons with tooltips and short labels.

---

# 18. Canvas Utilities

Add:

- zoom in
- zoom out
- fit view
- grid toggle
- snap toggle
- minimap toggle

Use a restrained compact floating utility area.

These controls can use glassmorphism.

---

# 19. Version Compare Screen

Create a professional comparison view.

Example:

```text
VERSION 04                    VERSION 05

01 Song A                     01 Song A
02 Song B                     02 Song C
03 Song C                     03 Song B
04 Song D                     04 Song D
```

Highlight moved items using subtle structural emphasis.

Do not create red/green "winning version" indicators.

The purpose is:

> **Understand what changed.**

Not:

> **Decide which version is better.**

---

# 20. Import / Integration Screen

Create a clean source-selection interface.

Possible cards:

```text
Spotify
Apple Music
YouTube Music
Tidal
Local / File Import
```

Use understated monochrome or brand-aware logos.

The screen should communicate that Flowbench is the editing layer, while external services are sources.

Do not build a streaming-service home page.

---

# 21. Context Menus

Design compact context menus for a selected card:

```text
Inspect
Lock
Move to section
Duplicate
Add note
Remove
```

Use clear keyboard hints where appropriate.

---

# 22. Modals / Dialogs

Create a few reusable dialog patterns:

- Rename playlist
- Create section
- Create version
- Import playlist
- Confirm destructive action

Dialogs should use dark glass surfaces with subtle borders.

Avoid giant modal windows.

---

# 23. Microinteractions

The prototype should suggest:

- card lift during drag
- smooth reordering
- connector re-routing
- subtle selection glow/outline
- inspector slide/fade
- section expand/collapse
- saved state confirmation
- tooltip appearance

Animations should be subtle and professional.

Avoid bouncing or playful animation.

---

# 24. Spacing and Grid

Use a consistent spacing system.

Suggested baseline:

```text
4
8
12
16
24
32
48
64
```

Keep alignments very deliberate.

The application should look engineered.

---

# 25. Iconography

Use a consistent thin-line icon system.

Examples:

- search
- playlist
- layers
- link
- lock
- unlock
- settings
- undo
- redo
- zoom
- grid
- eye
- more
- save
- plus
- drag handle

Avoid cute illustrated icons.

---

# 26. UX States to Prototype

Create visible states for:

### Track card
- default
- hover
- selected
- locked
- dragging

### Connector
- default
- hover
- selected

### Inspector
- hidden
- open
- modified

### Save
- saved
- saving
- unsaved changes

### Empty states
- no playlists
- no sections
- no selection

### Import
- source selection
- importing
- import complete
- import error

Use realistic static mockups.

---

# 27. Sample Data

Use believable fictional playlist content.

Example:

```text
Late Night Drive

01. Midnight City — M83
105 BPM · F♯ minor · 04:03

02. After Dark — Mr.Kitty
118 BPM · A minor · 04:17

03. Nightcall — Kavinsky
93 BPM · D minor · 04:18

04. Resonance — HOME
92 BPM · C major · 03:32
```

Use other songs if needed, but keep the metadata consistent.

The purpose is to create a believable prototype, not an actual streaming integration.

---

# 28. Important UX Constraint

Do not over-interpret metadata.

Never make the interface scream:

```text
GOOD
BAD
PERFECT
OPTIMAL
AI SCORE
```

Instead show objective-ish observations:

```text
+13 BPM
-0.12 Energy
Key shift
Large tempo change
Repeated artist
```

Users should interpret these themselves.

---

# 29. Figma Deliverable Structure

Organize the Figma file into pages/sections:

```text
00 — Cover / Design Direction
01 — Design Tokens
02 — Components
03 — App Shell
04 — Library
05 — Playlist Overview
06 — Flowbench Editor
07 — Inspectors / Overlays
08 — Version Compare
09 — Import / Settings
10 — Prototype Flow
```

Build reusable components and variants.

Use Auto Layout where appropriate.

Use consistent local styles/variables for:

- colors
- typography
- spacing
- radii
- borders
- shadows
- blur
- component states

---

# 30. Prototype Flow

Create a clickable Figma prototype for this primary journey:

```text
Welcome
  ↓
Playlist Library
  ↓
Open "Late Night Drive"
  ↓
Playlist Overview
  ↓
Open Flowbench
  ↓
Select Track 03
  ↓
Drag Track 03 between Track 01 and Track 02
  ↓
Inspect transition
  ↓
Create section
  ↓
Save Version 05
  ↓
Open Version Compare
```

A second optional journey:

```text
Library
  ↓
Import playlist
  ↓
Choose source
  ↓
Import complete
  ↓
Open Flowbench
```

---

# 31. Anti-Patterns to Avoid

Do NOT produce:

- generic purple AI gradients
- giant "AI-powered" hero sections
- SaaS dashboard KPI cards everywhere
- rainbow status chips
- excessive pill components
- excessive rounded rectangles
- cartoon illustrations
- excessive drop shadows
- enormous whitespace that wastes editor space
- mobile-first layouts
- generic Tailwind/shadcn-looking dashboards
- Spotify clone aesthetics
- giant glassmorphism everywhere
- overly futuristic cyberpunk neon UI

The application should be **quietly sophisticated**, not flashy.

---

# 32. Final Visual Target

Imagine a hybrid of:

**DaVinci Resolve node editor**
+
**modern editorial interface**
+
**industrial/technical design system**
+
**restrained retro color palette**
+
**professional music-production workstation**

But simplify it substantially.

The final result should feel like a tool that a serious music enthusiast could imagine actually using.

The design should make the central interaction immediately obvious:

> **Pick up a track card. Move it. Inspect what happens between it and the neighboring track. Shape the flow. Save the arrangement.**

Prioritize the Flowbench canvas above all other screens.

