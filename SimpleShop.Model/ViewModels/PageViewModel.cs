using System;

namespace Model.ViewModels;

public class PageViewModel
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int Count { get; set; }
    public int PageSize { get; set; }

    public PageViewModel(int count, int pageNumber, int pageSize)
    {
        Count = count;
        Page = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}