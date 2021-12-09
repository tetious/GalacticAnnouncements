using GalacticAnnouncements.API.Data;
using GalacticAnnouncements.API.Models;
using GalacticAnnouncements.API.TransferObjects;
using Marten;
using Marten.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace GalacticAnnouncements.API.Controllers;

public class AnnouncementController : ApiController
{
    private readonly IDocumentSession session;

    [HttpGet]
    public async Task<PagedCollection<AnnouncementDto>> List(int page, int pageSize = 100)
    {
        var announcements = await this.session.Query<Announcement>().ToPagedListAsync(page, pageSize);
        return new PagedCollection<AnnouncementDto>(announcements.Select(AnnouncementDto.FromAnnouncement),
            announcements.TotalItemCount);
    }

    public AnnouncementController(IDocumentSession session)
    {
        this.session = session;
    }
}
