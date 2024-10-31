namespace Laundry.Domain.Entities;

public class Feedback : BaseEntity
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public int Rating { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}
