using GalacticAnnouncements.API.Data;

namespace GalacticAnnouncements.API.Models;

public class Announcement : IEntity
{
    public Guid Id { get; init; }

    public string Author { get; init; }

    public string Subject { get; init; }

    public string Body { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime? UpdatedAt { get; init; }
}
