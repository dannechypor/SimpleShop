using System.Collections.Generic;
using Model.Models;

namespace Model.ViewModels;

public class IndexViewModel <T>
{
    public IEnumerable<T> Products { get; set; }
    public PageViewModel PageViewModel { get; set; }
    public FilterViewModel FilterViewModel { get; set; }
    public SortViewModel SortViewModel { get; set; }
}