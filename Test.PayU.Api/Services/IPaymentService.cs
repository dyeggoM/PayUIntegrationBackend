using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.PayU.Api.Services
{
    public interface IPaymentService
    {
        /// <summary>
        /// This method generates PayU signature.
        /// </summary>
        /// <param name="referenceCode">Order reference code</param>
        /// <param name="amount">Order amount</param>
        /// <param name="currency">Order currency</param>
        /// <returns></returns>
        string GetPaymentInformation(string referenceCode, string amount, string currency);

        /// <summary>
        /// This method generates PayU signature.
        /// </summary>
        /// <param name="referenceCode">Order reference code</param>
        /// <param name="amount">Order amount</param>
        /// <param name="currency">Order currency</param>
        /// <param name="statePol">State_pol</param>
        /// <returns></returns>
        string GetPaymentResponse(string referenceCode, string amount, string currency, string statePol);
    }
}
