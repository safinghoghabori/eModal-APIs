using Microsoft.AspNetCore.Mvc;
using Payment.API.Models;
using Payment.API.Services;

namespace Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest == null)
            {
                return BadRequest("Invalid payment request.");
            }

            try
            {
                var paymentConfirmation = await _paymentService.InitiatePaymentAsync(paymentRequest);
                return Ok(paymentConfirmation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your payment.");
            }
        }
    }
}
