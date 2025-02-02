using Store.Domain.Contracts.Services;
using Stripe;

namespace Store.Domain.Services;

public class PaymentService : IPaymentService
{
    public Task<PaymentIntent> CreateOrUpdatePaymentIntent()
    {
        throw new NotImplementedException();
    }
}