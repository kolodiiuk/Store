using Laundry.Domain.Contracts.Services;
using Laundry.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Laundry.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost("")]
    public async Task<ActionResult> CreatePaymentIntent([FromForm] CreatePaymentDto paymentDto)
    {
        throw new NotImplementedException();
    }
    // [HttpPost("webhook")]
    // public async Task<ActionResult> StripeWebhook()
    // {
    //     var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    //
    //     var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"],
    //         _config["StripeSettings:WhSecret"]);
    //
    //     var charge = (Charge)stripeEvent.Data.Object;
    //
    //     var order = await _context.Orders.FirstOrDefaultAsync(x => 
    //         x.PaymentIntentId == charge.PaymentIntentId);
    //
    //     if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.PaymentReceived;
    //
    //     await _context.SaveChangesAsync();
    //
    //     return new EmptyResult();
    // } 
}