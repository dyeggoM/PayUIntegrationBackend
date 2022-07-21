using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Test.PayU.Api.Entities;
using Test.PayU.Api.Services;

namespace Test.PayU.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayuController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public IPaymentService _paymentService;
        private readonly ILogger<PayuController> _logger;
        public PayuController(ILogger<PayuController> logger, IPaymentService paymentService, IConfiguration configuration)
        {
            _logger = logger;
            _paymentService = paymentService;
            _configuration = configuration;
        }

        /// <summary>
        /// This method allows Payu to inform the state of the payment,
        /// </summary>
        /// <param name="confirmation">Payu response data.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("confirmation")]
        public IActionResult PayuConfirmation([FromForm] PayuConfirmation confirmation)
        {
            try
            {
                _logger.Log(LogLevel.Information, $"Hit: {nameof(PayuController)}.{nameof(PayuConfirmation)}: {JsonSerializer.Serialize(confirmation)}");
                if (string.IsNullOrWhiteSpace(confirmation.reference_sale) || string.IsNullOrWhiteSpace(confirmation.value) || string.IsNullOrWhiteSpace(confirmation.currency) || string.IsNullOrWhiteSpace(confirmation.sign))
                    return BadRequest();
                var signature = _paymentService.GetPaymentResponse(confirmation.reference_sale, confirmation.value, confirmation.currency, confirmation.state_pol);
                if (signature != confirmation.sign)
                    return BadRequest();
                _logger.Log(LogLevel.Information, $"PayU confirmation received with signature: {signature}");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"{nameof(PayuController)}.{nameof(PayuConfirmation)}: {JsonSerializer.Serialize(e)}");
                return BadRequest();
            }
        }

        /// <summary>
        /// This method gets the signature for the payment.
        /// </summary>
        /// <param name="request">Order details.</param>
        /// <returns></returns>
        [HttpGet("payment-signature")]
        public IActionResult GetPaymentSignature([FromQuery] PayuPaymentRequest request)
        {
            try
            {
                var referenceCode = Guid.NewGuid().ToString("N");
                var response = new PayuPaymentInformation()
                {
                    amount = request.amount,
                    currency = request.currency,
                    referenceCode = referenceCode,
                    signature = _paymentService.GetPaymentInformation(referenceCode, request.amount.ToString(), request.currency)
                };
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"{nameof(PayuController)}.{nameof(GetPaymentSignature)}: {JsonSerializer.Serialize(e)}");
                return BadRequest();
            }
        }
    }
}
