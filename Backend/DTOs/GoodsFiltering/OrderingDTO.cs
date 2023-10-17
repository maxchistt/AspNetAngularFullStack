namespace Backend.DTOs.GoodsFiltering
{
    public record OrderingDTO(
        string? OrderBy = null,
        bool OrderByDescending = false
    );
}