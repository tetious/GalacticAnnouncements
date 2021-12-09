using GalacticAnnouncements.API.Models;

namespace GalacticAnnouncements.API.TransferObjects;

public record AnnouncementDto(Guid Id, string Author, LocalDate Date, string Subject, string Body)
{
    public static AnnouncementDto FromAnnouncement(Announcement announcement)
    {
        return new AnnouncementDto(announcement.Id, announcement.Author, announcement.Date, announcement.Subject,
            announcement.Body);
    }
}
