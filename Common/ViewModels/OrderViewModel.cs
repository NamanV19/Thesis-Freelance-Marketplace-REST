using Common.NavModels;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid CatalogId { get; set; }
        public Guid FreelancerId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public virtual PaymentPostModel Payment { get; set; }
        public virtual ReviewPostModel Review { get; set; }

        // From other services
        public FreelancerNavModel Freelancer { get; set; }
        public CatalogViewModel Catalog { get; set; }
    }
}
