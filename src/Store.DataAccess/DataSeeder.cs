using Store.Domain.Entities;
using Store.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Store.DataAccess;

public class DataSeeder
{
    private static readonly string[] FirstNames = { "John", "Emma", "Michael", "Sarah", "David", "Lisa", "James", "Anna", "Robert", "Maria" };
    private static readonly string[] LastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
    private static readonly string[] Streets = { "Main Street", "Park Avenue", "Broadway", "5th Avenue", "Oak Lane", "Maple Street", "Cedar Road", "Pine Street" };
    private static readonly string[] Districts = { "Downtown", "Uptown", "Midtown", "West End", "East Side", "North District", "South District" };
    private static readonly string[] ProductDescriptions = {
        "Premium product for delicate items",
        "Quick turnaround product",
        "Eco-friendly cleaning option",
        "Professional care for special materials",
        "Express product with same-day delivery",
        "Deep cleaning treatment",
        "Gentle care for sensitive items",
        "Specialized stain removal product"
    };

    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!await context.Users.AnyAsync())
        {
            await SeedUsers(context);
        }

        if (!await context.Addresses.AnyAsync())
        {
            await SeedAddresses(context);
        }

        if (!await context.Products.AnyAsync())
        {
            await SeedProducts(context);
        }

        if (!await context.Coupons.AnyAsync())
        {
            await SeedCoupons(context);
        }

        if (!await context.ProductCoupons.AnyAsync())
        {
            await SeedProductCoupons(context);
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

    private static async Task SeedUsers(AppDbContext context)
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
                PasswordHash = "hashed_password",
                Role = i == 0 ? Role.Admin : Role.User
            });
        }
        await context.Users.AddRangeAsync(userList);
        await context.SaveChangesAsync();
    }

    private static async Task SeedAddresses(AppDbContext context)
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

    private static async Task SeedProducts(AppDbContext context)
    {
        var productList = new List<Product>
        {
            new() { Name = "Regular Wash & Fold", Category = ProductCategory.Washing, Description = ProductDescriptions[0], Price = 10.00m, UnitType = UnitType.Kg, IsAvailable = true },
            new() { Name = "Premium Dry Cleaning", Category = ProductCategory.DryCleaning, Description = ProductDescriptions[1], Price = 15.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Express Ironing", Category = ProductCategory.Ironing, Description = ProductDescriptions[2], Price = 5.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Bedding & Linens", Category = ProductCategory.Washing, Description = ProductDescriptions[3], Price = 20.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Suit Cleaning", Category = ProductCategory.DryCleaning, Description = ProductDescriptions[4], Price = 25.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Stain Removal", Category = ProductCategory.AddOn, Description = ProductDescriptions[5], Price = 12.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Curtain Cleaning", Category = ProductCategory.DryCleaning, Description = ProductDescriptions[6], Price = 30.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Shoe Cleaning", Category = ProductCategory.AddOn, Description = ProductDescriptions[7], Price = 18.00m, UnitType = UnitType.Pair, IsAvailable = true },
            new() { Name = "Bulk Washing", Category = ProductCategory.Washing, Description = "Economical bulk washing product", Price = 8.00m, UnitType = UnitType.Kg, IsAvailable = true },
            new() { Name = "Wedding Dress Care", Category = ProductCategory.AddOn, Description = "Special care for wedding dresses", Price = 99.99m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Sports Gear Cleaning", Category = ProductCategory.AddOn, Description = "Specialized cleaning for sports equipment", Price = 15.00m, UnitType = UnitType.Piece, IsAvailable = true },
            new() { Name = "Premium Ironing", Category = ProductCategory.Ironing, Description = "Detailed ironing product", Price = 7.50m, UnitType = UnitType.Piece, IsAvailable = true }
        };
        await context.Products.AddRangeAsync(productList);
        await context.SaveChangesAsync();
    }

    private static async Task SeedOrders(AppDbContext context)
    {
        var random = new Random();
        var users = await context.Users.Include(u => u.Addresses).ToListAsync();
        var products = await context.Products.ToListAsync();
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
                var product = products[random.Next(products.Count)];
                var quantity = random.Next(1, 5);
                var total = product.Price * quantity;
                subtotal += total;

                await context.OrderItems.AddAsync(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    CurrentUnitPrice = product.Price,
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
                    Comment = $"Great product! Order #{order.Id}",
                    Created = order.DeliveredDate?.AddDays(1) ?? DateTime.Now
                });
            }
        }
        await context.SaveChangesAsync();
    }

    private static async Task SeedBasketItems(AppDbContext context)
    {
        var random = new Random();
        var users = await context.Users.ToListAsync();
        var products = await context.Products.ToListAsync();
        var randomUsers = users.OrderBy(_ => random.Next()).Take(5).ToList();
        foreach (var user in randomUsers)
        {
            var itemCount = random.Next(1, 4);
            for (int i = 0; i < itemCount; i++)
            {
                var product = products[random.Next(products.Count)];
                var quantity = random.Next(1, 3);
                await context.BasketItems.AddAsync(new BasketItem
                {
                    UserId = user.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                });
            }
        }
        await context.SaveChangesAsync();
    }

    private static string GenerateCouponCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, 8)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    private static async Task SeedCoupons(AppDbContext context)
    {
        var random = new Random();
        var coupons = new List<Coupon>();
        
        // Add some active coupons
        for (int i = 0; i < 5; i++)
        {
            coupons.Add(new Coupon
            {
                Code = GenerateCouponCode(),
                Percentage = random.Next(5, 31), // 5% to 30% discount
                StartDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(20),
                UsedCount = random.Next(0, 50)
            });
        }

        // Add some expired coupons
        for (int i = 0; i < 3; i++)
        {
            coupons.Add(new Coupon
            {
                Code = GenerateCouponCode(),
                Percentage = random.Next(5, 31),
                StartDate = DateTime.Now.AddDays(-40),
                EndDate = DateTime.Now.AddDays(-10),
                UsedCount = random.Next(10, 100)
            });
        }

        // Add some future coupons
        for (int i = 0; i < 2; i++)
        {
            coupons.Add(new Coupon
            {
                Code = GenerateCouponCode(),
                Percentage = random.Next(5, 31),
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(40),
                UsedCount = 0
            });
        }

        await context.Coupons.AddRangeAsync(coupons);
        await context.SaveChangesAsync();
    }

    private static async Task SeedProductCoupons(AppDbContext context)
    {
        var random = new Random();
        var products = await context.Products.ToListAsync();
        var coupons = await context.Coupons.ToListAsync();
        var productCoupons = new List<ProductCoupon>();

        // Assign 1-3 random products to each coupon
        foreach (var coupon in coupons)
        {
            var productCount = random.Next(1, 4);
            var randomProducts = products.OrderBy(x => random.Next()).Take(productCount);
            
            foreach (var product in randomProducts)
            {
                productCoupons.Add(new ProductCoupon
                {
                    ProductId = product.Id,
                    CouponId = coupon.Id
                });
            }
        }

        await context.ProductCoupons.AddRangeAsync(productCoupons);
        await context.SaveChangesAsync();
    }
}