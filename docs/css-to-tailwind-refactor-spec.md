# Playlist Flowbench

## Project

Playlist Flowbench is a visual playlist sequencing workbench.

## Stack

- C#
- ASP.NET Core
- Blazor
- Tailwind CSS
- No React
- No Vue
- No Angular
- No JavaScript framework unless explicitly required

## Architecture

Prefer modular, reusable Blazor components.

Keep UI components focused and composable.

Do not place large amounts of application logic directly inside .razor markup.

Keep domain/application logic separate from presentation concerns.

## Design Source of Truth

Figma is the visual source of truth.

When implementing a screen from Figma:
- inspect the relevant Figma frame using the Figma MCP
- preserve layout hierarchy
- preserve spacing
- preserve typography hierarchy
- preserve component structure
- preserve the established color palette
- preserve interaction intent

Do not invent additional UI elements simply because they are technically possible.

## UI/UX

Keep the interface minimal and utilitarian.

Avoid:
- unnecessary descriptions
- redundant labels
- excessive badges
- excessive cards
- fake dashboard metrics
- decorative Unicode symbols
- AI-generic visual patterns
- unnecessary animations

Prefer:
- clear hierarchy
- short labels
- progressive disclosure
- contextual information
- whitespace
- predictable interactions

## Tailwind

Use Tailwind utility classes for styling.

Do not create large custom CSS blocks when Tailwind can express the design cleanly.

Do not reintroduce the old CSS architecture unless there is a specific reason.

## Verification

After implementation:
1. Build the project.
2. Fix compilation errors.
3. Verify the affected page.
4. Review the implementation against the Figma frame.
5. Keep changes scoped to the requested functionality.