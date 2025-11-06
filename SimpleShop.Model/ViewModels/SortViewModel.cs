public enum SortState
{
    NameAsc,
    NameDesc,
    PriceAsc,
    PriceDesc
}

namespace Model.Models
{
    public class SortViewModel(SortState sortOrder)
    {
        public SortState NameSort { get; private set; } = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
        public SortState PriceSort { get; private set; } = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
        public SortState Current { get; private set; } = sortOrder;
    }
}