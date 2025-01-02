using Stripe;
namespace Laundry.Domain.Contracts.Services;

public interface IPaymentService
{
   Task<PaymentIntent> CreateOrUpdatePaymentIntent();
   
}