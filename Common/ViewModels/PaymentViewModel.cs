using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class PaymentViewModel
    {
        public Guid Id { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; }
        public Guid OrderId { get; set; }
        public virtual OrderPostModel Order { get; set; }
    }
}
