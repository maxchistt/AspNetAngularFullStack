using Backend.Shared.Other;

namespace Backend.DTOs.Other
{
    public record PaginatedListDTO<T>(ICollection<T> Items, int TotalItemsCount, int PageIndex, int PageSize)
    {
        public int TotalPagesCount { get => TotalItemsCount.DivideWithRoundingUp(PageSize); }
    }
}