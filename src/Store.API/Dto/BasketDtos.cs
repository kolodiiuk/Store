namespace Store.API.Dto;

public class CreateBasketItemDto
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }
}

public class UpdateQuantityDto
{
    public int BasketItemId { get; set; }
    public int NewValue { get; set; }
}