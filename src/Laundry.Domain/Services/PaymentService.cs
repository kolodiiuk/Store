using Laundry.Domain.Contracts.Services;
using Stripe;

namespace Laundry.Domain.Services;

public class PaymentService : IPaymentService
{
    public Task<PaymentIntent> CreateOrUpdatePaymentIntent()
    {
        throw new NotImplementedException();
    }
}