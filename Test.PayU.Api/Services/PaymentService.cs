using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Test.PayU.Api.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// This method generates PayU signature
        /// </summary>
        /// <param name="referenceCode">Order reference code</param>
        /// <param name="amount">Order amount</param>
        /// <param name="currency">Order currency</param>
        /// <returns></returns>
        public string GetPaymentInformation(string referenceCode, string amount, string currency)
        {
            try
            {
                var merchantId = _configuration["PayUConfiguration:MerchantId"];
                var apiKey = _configuration["PayUConfiguration:ApiKey"];
                var preHashString = $"{apiKey}~{merchantId}~{referenceCode}~{amount}~{currency}";
                return HashString(preHashString);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"{nameof(PaymentService)}.{nameof(GetPaymentInformation)}: {JsonSerializer.Serialize(e)}");
                return "";
            }
        }


        /// <summary>
        /// This method generates PayU signature
        /// </summary>
        /// <param name="referenceCode">Order reference code</param>
        /// <param name="amount">Order amount</param>
        /// <param name="currency">Order currency</param>
        /// <param name="statePol">State_pol</param>
        /// <returns></returns>
        public string GetPaymentResponse(string referenceCode, string amount, string currency, string statePol)
        {
            try
            {
                var merchantId = _configuration["PayUConfiguration:MerchantId"];
                var apiKey = _configuration["PayUConfiguration:ApiKey"];
                var curedAmount = amount.Last() == '0' ? amount.Substring(0, amount.LastIndexOf('0')) : amount;
                var preHashString = $"{apiKey}~{merchantId}~{referenceCode}~{curedAmount}~{currency}~{statePol}";
                return HashString(preHashString);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"{nameof(PaymentService)}.{nameof(GetPaymentResponse)}: {JsonSerializer.Serialize(e)}");
                return "";
            }
        }

        private string HashString(string preHashString)
        {
            using (var sha256Hash = SHA256.Create())
            {
                var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(preHashString));
                var stringBuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}
