using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class PaymentPostModel
    {
        [Required]
        public string PaymentMethod { get; set; } // [GoPay, Ovo, PayPal]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Guid OrderId { get; set; }
    }
}
