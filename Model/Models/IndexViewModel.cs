using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class IndexViewModel <T>
    {
        public IEnumerable<T> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
    }
}
