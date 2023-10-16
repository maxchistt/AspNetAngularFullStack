namespace Backend.DTOs.Goods
{
    public record GoodsFilteringParamsDto(int? CategoryId = null, int? PageIndex = null, int? PageSize = null, bool WithAmount = false, string? orderBy = null);
}
