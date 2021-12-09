using GalacticAnnouncements.API.Models;

namespace GalacticAnnouncements.API.TransferObjects;

public record AnnouncementDto(string Author, DateTime Date, string Subject, string Body)
{
    public static AnnouncementDto FromAnnouncement(Announcement announcement)
    {
        return new AnnouncementDto(announcement.Author, announcement.UpdatedAt ?? announcement.CreatedAt,
            announcement.Subject, announcement.Body);
    }
}
