# Initial domain models

See [the UML class diagram and database planning notes](../../../docs/architecture/class-diagram.md).

These five POCO classes establish the initial domain vocabulary. They are not yet connected to EF Core or the UI; `Models/` contains the existing presentation projections. IDs and UTC timestamps receive defaults, collections start empty, and nullable navigations support unloaded relationships.

Before persistence, implement the documented validation, ordering, section membership, ownership, locking, deletion, and concurrency rules. `required` members provide C# construction checks, not runtime input validation. Foreign keys and navigation properties must be kept consistent by the future application/persistence layer.
