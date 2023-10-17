namespace Backend.Shared.Other
{
    public record PaginatedList<T>(IEnumerable<T> Items, int TotalItemsCount, int PageIndex, int PageSize)
    {
        public int TotalPagesCount { get => TotalItemsCount.DivideWithRoundingUp(PageSize); }
    }
}