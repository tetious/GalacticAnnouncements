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
    public async Task<ActionResult<PagedCollection<AnnouncementDto>>> List(int page, int pageSize = 100)
    {
        var announcements = await this.session.Query<Announcement>().ToPagedListAsync(page, pageSize);
        return new PagedCollection<AnnouncementDto>(announcements.Select(AnnouncementDto.FromAnnouncement),
            announcements.TotalItemCount);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnnouncementDto>> Get(Guid id)
    {
        var existing = await this.session.LoadAsync<Announcement>(id);
        if (existing == null) return this.NotFound();

        return AnnouncementDto.FromAnnouncement(existing);
    }

    [HttpPost]
    public async Task<ActionResult<AnnouncementDto>> Create([FromBody] AnnouncementUpdateRequest request)
    {
        var announcement = new Announcement
        {
            Author = request.Author,
            Subject = request.Subject,
            Body = request.Body,
            Date = request.Date,
            CreatedAt = this.clock.GetCurrentInstant()
        };

        this.session.Insert(announcement);
        await this.session.SaveChangesAsync();

        return AnnouncementDto.FromAnnouncement(announcement);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AnnouncementDto>> Update(Guid id, [FromBody] AnnouncementUpdateRequest request)
    {
        var existing = await this.session.LoadAsync<Announcement>(id);
        if (existing == null) return this.NotFound();

        var updated = existing with
        {
            Author = request.Author,
            Subject = request.Subject,
            Body = request.Body,
            Date = request.Date,
            UpdatedAt = this.clock.GetCurrentInstant()
        };

        this.session.Update(updated);
        await this.session.SaveChangesAsync();

        return AnnouncementDto.FromAnnouncement(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        this.session.Delete<Announcement>(id);
        await this.session.SaveChangesAsync();

        return this.Accepted();
    }

    private readonly IDocumentSession session;
    private readonly IClock clock;

    public AnnouncementController(IDocumentSession session, IClock clock)
    {
        this.session = session;
        this.clock = clock;
    }
}
