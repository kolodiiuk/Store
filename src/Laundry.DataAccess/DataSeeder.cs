using Laundry.Domain.Entities;
using Laundry.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Laundry.DataAccess;

public class DataSeeder
{
    private static readonly string[] FirstNames = { "John", "Emma", "Michael", "Sarah", "David", "Lisa", "James", "Anna", "Robert", "Maria" };
    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
    private static readonly string[] Streets = { "Main Street", "Park Avenue", "Broadway", "5th Avenue", "Oak Lane", "Maple Street", "Cedar Road", "Pine Street" };
    private static readonly string[] Districts = { "Downtown", "Uptown", "Midtown", "West End", "East Side", "North District", "South District" };
    private static readonly string[] ServiceDescriptions = {
        "Premium service for delicate items",
        "Quick turnaround service",
        "Eco-friendly cleaning option",
        "Professional care for special materials",
        "Express service with same-day delivery",
        "Deep cleaning treatment",
        "Gentle care for sensitive items",
        "Specialized stain removal service"
    };

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<LaundryDbContext>();

        if (!await context.Users.AnyAsync())
        {
            await SeedUsers(context);
        }

        if (!await context.Addresses.AnyAsync())
        {
            await SeedAddresses(context);
        }

        if (!await context.Services.AnyAsync())
        {
            await SeedServices(context);
        }

        if (!await context.Orders.AnyAsync())
        {
            await SeedOrders(context);
        }

        if (!await context.BasketItems.AnyAsync())
        {
            await SeedBasketItems(context);
        }
    }

    private static async Task SeedUsers(LaundryDbContext context)
    {
        var userList = new List<User>();
        for (int i = 0; i < 10; i++)
        {
            userList.Add(new User
            {
                FirstName = FirstNames[i],
                LastName = LastNames[i],
                Email = $"{FirstNames[i].ToLower()}.{LastNames[i].ToLower()}@example.com",
                PhoneNumber = $"555{i:D4}1234",
                Password = "hashed_password",
                Role = i == 0 ? Role.Admin : Role.User
            });
        }
        await context.Users.AddRangeAsync(userList);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAddresses(LaundryDbContext context)
    {
        var random = new Random();
        var users = await context.Users.ToListAsync();
        foreach (var user in users)
        {
            var addressCount = random.Next(1, 4);
            for (int i = 0; i < addressCount; i++)
            {
                await context.Addresses.AddAsync(new Address
                {
                    UserId = user.Id,
                    Apartments = $"Apt {random.Next(100, 999)}",
                    House = $"Building {(char)('A' + random.Next(0, 6))}",
                    Street = Streets[random.Next(Streets.Length)],
                    District = Districts[random.Next(Districts.Length)],
                    City = "New York"
                });
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedServices(LaundryDbContext context)
    {
        var serviceList = new List<Service>
        {
            new() { Name = "Regular Wash & Fold", Category = ServiceCategory.Washing, Description = ServiceDescriptions[0], PricePerUnit = 10.00m, UnitType = UnitType.Kg, IsAvailable = true },
            new() { Name = "Premium Dry Cleaning", Category = ServiceCategory.DryCleaning, Description = ServiceDescriptions[1], PricePerUnit = 15.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Express Ironing", Category = ServiceCategory.Ironing, Description = ServiceDescriptions[2], PricePerUnit = 5.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Bedding & Linens", Category = ServiceCategory.Washing, Description = ServiceDescriptions[3], PricePerUnit = 20.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Suit Cleaning", Category = ServiceCategory.DryCleaning, Description = ServiceDescriptions[4], PricePerUnit = 25.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Stain Removal", Category = ServiceCategory.AddOn, Description = ServiceDescriptions[5], PricePerUnit = 12.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Curtain Cleaning", Category = ServiceCategory.DryCleaning, Description = ServiceDescriptions[6], PricePerUnit = 30.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Shoe Cleaning", Category = ServiceCategory.AddOn, Description = ServiceDescriptions[7], PricePerUnit = 18.00m, UnitType = UnitType.Pair, IsAvailable = true },
            new() { Name = "Bulk Washing", Category = ServiceCategory.Washing, Description = "Economical bulk washing service", PricePerUnit = 8.00m, UnitType = UnitType.Kg, IsAvailable = true },
            new() { Name = "Wedding Dress Care", Category = ServiceCategory.AddOn, Description = "Special care for wedding dresses", PricePerUnit = 99.99m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Sports Gear Cleaning", Category = ServiceCategory.AddOn, Description = "Specialized cleaning for sports equipment", PricePerUnit = 15.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Premium Ironing", Category = ServiceCategory.Ironing, Description = "Detailed ironing service", PricePerUnit = 7.50m, UnitType = UnitType.Piece, IsAvailable = true }
        };
        await context.Services.AddRangeAsync(serviceList);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(LaundryDbContext context)
    {
        var random = new Random();
        var users = await context.Users.Include(u => u.Addresses).ToListAsync();
        var services = await context.Services.ToListAsync();
        var statuses = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>().ToArray();
        var paymentMethods = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().ToArray();

        for (int i = 0; i < 20; i++)
        {
            var user = users[random.Next(users.Count)];
            var address = user.Addresses.ElementAt(random.Next(user.Addresses.Count));
            var status = statuses[random.Next(statuses.Length)];
            var daysAgo = random.Next(1, 60);

            var order = new Order
            {
                UserId = user.Id,
                AddressId = address.Id,
                Status = status,
                PaymentMethod = paymentMethods[random.Next(paymentMethods.Length)],
                PaymentStatus = PaymentStatus.Paid,
                Discount = 0,
                Description = $"Order #{i + 1}",
                DeliveryFee = 5.00m,
                CreatedAt = DateTime.Now.AddDays(-daysAgo),
                CollectedDate = status != OrderStatus.Collected ? DateTime.Now.AddDays(-daysAgo + 1) : null,
                DeliveredDate = status == OrderStatus.Delivered ? DateTime.Now.AddDays(-daysAgo + 3) : null
            };

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            // Add 2-5 items per order
            var itemCount = random.Next(2, 6);
            decimal subtotal = 0;
            for (int j = 0; j < itemCount; j++)
            {
                var service = services[random.Next(services.Count)];
                var quantity = random.Next(1, 5);
                var total = service.PricePerUnit * quantity;
                subtotal += total;

                await context.OrderItems.AddAsync(new OrderItem
                {
                    OrderId = order.Id,
                    ServiceId = service.Id,
                    Quantity = quantity,
                    CurrentUnitPrice = service.PricePerUnit,
                    Total = total
                });
            }

            order.Subtotal = subtotal;

            // Add feedback for completed orders
            if (status == OrderStatus.Delivered)
            {
                await context.Feedbacks.AddAsync(new Feedback
                {
                    OrderId = order.Id,
                    Rating = random.Next(3, 6),
                    Comment = $"Great service! Order #{order.Id}",
                    Created = order.DeliveredDate?.AddDays(1) ?? DateTime.Now
                });
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedBasketItems(LaundryDbContext context)
    {
        var random = new Random();
        var users = await context.Users.ToListAsync();
        var services = await context.Services.ToListAsync();
        var randomUsers = users.OrderBy(x => random.Next()).Take(5).ToList();
        foreach (var user in randomUsers)
        {
            var itemCount = random.Next(1, 4);
            for (int i = 0; i < itemCount; i++)
            {
                var service = services[random.Next(services.Count)];
                var quantity = random.Next(1, 3);
                await context.BasketItems.AddAsync(new BasketItem
                {
                    UserId = user.Id,
                    ServiceId = service.Id,
                    Quantity = quantity,
                });
            }
        }
        await context.SaveChangesAsync();
    }
}