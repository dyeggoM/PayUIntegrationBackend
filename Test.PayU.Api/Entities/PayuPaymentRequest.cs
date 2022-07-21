using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.PayU.Api.Entities
{
    public class PayuPaymentRequest
    {
        public decimal amount { get; set; }
        public string currency { get; set; }
    }
}
