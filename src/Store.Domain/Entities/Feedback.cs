using System.Text.Json.Serialization;

namespace Store.Domain.Entities;

public class Feedback : BaseEntity
{
    public int Rating { get; set; }
    public string Comment { get; set; }
    public int OrderId { get; set; }
    public DateTime Created { get; set; }
    
    [JsonIgnore]
    public Order Order { get; set; }
}
