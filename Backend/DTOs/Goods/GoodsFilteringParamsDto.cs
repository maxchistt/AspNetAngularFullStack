namespace Backend.DTOs.Goods
{
    public record GoodsFilteringParamsDTO(int? CategoryId = null, int? PageIndex = null, int? PageSize = null, bool WithAmount = false, string? orderBy = null);
}
