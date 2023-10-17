namespace Backend.DTOs.GoodsFiltering
{
    public record PriceFilteringDTO(
        decimal? MinPrice = null,
        decimal? MaxPrice = null
    );
}