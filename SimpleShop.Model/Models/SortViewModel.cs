using System;
using System.Collections.Generic;
using System.Text;

public enum SortState
{
    NameAsc,   
    NameDesc,  
    PriceAsc, 
    PriceDesc,    

}
namespace Model.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; private set; } // значение для сортировки по имени
        public SortState PriceSort { get; private set; }    // значение для сортировки по price
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
