using GalacticAnnouncements.API.Data;

namespace GalacticAnnouncements.API.Models;

public record Announcement : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Author { get; init; }

    public string Subject { get; init; }

    public string Body { get; init; }

    public LocalDate Date { get; init; }

    public Instant CreatedAt { get; init; }

    public Instant? UpdatedAt { get; init; }
}
