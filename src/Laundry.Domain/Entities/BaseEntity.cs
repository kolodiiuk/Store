using System.ComponentModel.DataAnnotations;

namespace Laundry.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    [Timestamp]
    public byte[] RowVersion { get; set; }
}
