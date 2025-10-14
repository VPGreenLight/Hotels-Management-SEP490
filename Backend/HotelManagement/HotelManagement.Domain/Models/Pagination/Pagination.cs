namespace HotelManagement.Domain.Models.Pagination;

public class Pagination<T> where T : class
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }

    public int TotalPages
    {
        get
        {
            if (PageSize == 0) return 0;
            return (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public ICollection<T>? Items { get; set; } = new List<T>();
}