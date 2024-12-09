namespace Laundry.Domain.Entities;

public class Feedback : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int OrderId { get; set; }
    public DateTime Created { get; set; }
    
    public Order Order { get; set; }
}
