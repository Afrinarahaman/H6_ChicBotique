﻿namespace H6_ChicBotique.DTOs
{
    public class ShippingDetailsRequest
    {
        public string Address { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
    }
}
