namespace GalacticAnnouncements.API.Data;

public class PagedCollection<T>
{
    public PagedCollection(IEnumerable<T> list, long totalCount)
    {
        this.List = list;
        this.TotalCount = totalCount;
    }

    public IEnumerable<T> List { get; }

    public long TotalCount { get; }
}

public static class PagedCollectionExtensions
{
    public static PagedCollection<T> ToPagedCollection<T>(this IEnumerable<T> that, long count)
    {
        return new PagedCollection<T>(that.ToList(), count);
    }
}
