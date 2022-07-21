using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.PayU.Api.Entities
{
    public class PayuConfirmation
    {
        public string merchant_id { get; set; }
        public string state_pol { get; set; }
        public string risk { get; set; }
        public string response_code_pol { get; set; }
        public string reference_sale { get; set; }
        public string reference_pol { get; set; }
        public string sign { get; set; }
        public string extra1 { get; set; }
        public string extra2 { get; set; }
        public string payment_method { get; set; }
        public string payment_method_type { get; set; }
        public string installments_number { get; set; }
        public string value { get; set; }
        public string tax { get; set; }
        public string additional_value { get; set; }
        public string transaction_date { get; set; }
        public string currency { get; set; }
        public string email_buyer { get; set; }
        public string cus { get; set; }
        public string pse_bank { get; set; }
        public string test { get; set; }
        public string description { get; set; }
        public string billing_address { get; set; }
        public string shipping_address { get; set; }
        public string phone { get; set; }
        public string office_phone { get; set; }
        public string account_number_ach { get; set; }
        public string account_type_ach { get; set; }
        public string administrative_fee { get; set; }
        public string administrative_fee_base { get; set; }
        public string administrative_fee_tax { get; set; }
        public string airline_code { get; set; }
        public string attempts { get; set; }
        public string authorization_code { get; set; }
        public string travel_acency_authorization_code { get; set; }
        public string bank_id { get; set; }
        public string billing_city { get; set; }
        public string billing_country { get; set; }
        public string commision_pol { get; set; }
        public string commision_pol_currency { get; set; }
        public string customer_number { get; set; }
        public string date { get; set; }
        public string error_code_bank { get; set; }
        public string error_message_bank { get; set; }
        public string exchange_rate { get; set; }
        public string ip { get; set; }
        public string nickname_buyer { get; set; }
        public string nickname_seller { get; set; }
        public string payment_method_id { get; set; }
        public string payment_request_state { get; set; }
        public string pseReference1 { get; set; }
        public string pseReference2 { get; set; }
        public string pseReference3 { get; set; }
        public string response_message_pol { get; set; }
        public string shipping_city { get; set; }
        public string shipping_country { get; set; }
        public string transaction_bank_id { get; set; }
        public string transaction_id { get; set; }
        public string payment_method_name { get; set; }
    }
}
