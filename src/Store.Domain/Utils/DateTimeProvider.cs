namespace Store.Domain.Utils;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}

public class CurrentDateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
}
