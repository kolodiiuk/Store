using Stripe;
namespace Store.Domain.Contracts.Services;

public interface IPaymentService
{
   Task<PaymentIntent> CreateOrUpdatePaymentIntent();
   
}