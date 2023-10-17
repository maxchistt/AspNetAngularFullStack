namespace Backend.DTOs.GoodsFiltering
{
    public record PaginationDTO(
        int? PageIndex = null,
        int? PageSize = null
    );
}