using System.ComponentModel.DataAnnotations;

namespace GalacticAnnouncements.API.TransferObjects;

public record AnnouncementUpdateRequest(
    [Required] string Author,
    [Required] LocalDate Date,
    [Required] string Subject,
    [Required] string Body
);
