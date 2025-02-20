using Store.Domain.Contracts.Services;
using Stripe;

namespace Store.Services.Services;

public class PaymentService : IPaymentService
{
    public Task<PaymentIntent> CreateOrUpdatePaymentIntent()
    {
        throw new NotImplementedException();
    }
}