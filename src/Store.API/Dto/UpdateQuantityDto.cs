namespace Store.API.Dto;

public class UpdateQuantityDto
{
    public int BasketItemId { get; set; }
    public int NewValue { get; set; }
}