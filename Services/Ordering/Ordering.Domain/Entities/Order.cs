﻿using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Entities
{
    public class Order:EntityBase
    {
        public string Username { get; set; }
        public decimal TotalPrice { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string BankName { get; set; }
        public string RefCode { get; set; }
        public int PaymentMethod { get; set; }
    }
}
