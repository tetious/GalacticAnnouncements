using GalacticAnnouncements.API.Data;
using GalacticAnnouncements.API.Models;
using GalacticAnnouncements.API.TransferObjects;
using Marten;
using Marten.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace GalacticAnnouncements.API.Controllers;

public class AnnouncementController : ApiController
{
    [HttpGet]
    public async Task<PagedCollection<AnnouncementDto>> List(int page, int pageSize = 100)
    {
        var announcements = await this.session.Query<Announcement>().ToPagedListAsync(page, pageSize);
        return new PagedCollection<AnnouncementDto>(announcements.Select(AnnouncementDto.FromAnnouncement),
            announcements.TotalItemCount);
    }

    [HttpPost]
    public async Task<AnnouncementDto> Create([FromBody] AnnouncementUpdateRequest create)
    {
        var announcement = new Announcement
        {
            Author = create.Author,
            Subject = create.Subject,
            Body = create.Body,
            Date = create.Date,
            CreatedAt = this.clock.GetCurrentInstant()
        };
        this.session.Insert(announcement);

        await this.session.SaveChangesAsync();

        return AnnouncementDto.FromAnnouncement(announcement);
    }

    private readonly IDocumentSession session;
    private readonly IClock clock;

    public AnnouncementController(IDocumentSession session, IClock clock)
    {
        this.session = session;
        this.clock = clock;
    }
}
