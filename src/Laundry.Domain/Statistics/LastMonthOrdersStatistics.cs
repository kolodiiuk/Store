namespace Laundry.Domain.Statistics;

public class LastMonthOrdersStatistics
{
    public int Day { get; set; }
    public int OrderCountPerDay { get; set; }
    public decimal TotalPerDay { get; set; }
}

// 1) про послуги, що замовляють найчастіше у такому вигляді:
// назва послуги, її тип,
// вартість послуги, одиниця, в якій вона вимірюється,
// кількість замовлень,
// у яких вона була замовлена (обчислюється),
// кількість клієнтів, що замовили послугу (обчислюється);
