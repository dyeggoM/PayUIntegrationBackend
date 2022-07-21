using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.PayU.Api.Entities
{
    public class PayuPaymentInformation
    {
        public string referenceCode { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public string signature { get; set; }
    }
}
