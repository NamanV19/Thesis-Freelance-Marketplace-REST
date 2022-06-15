using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class OrderPostModel
    {
        [Required]
        public Guid CatalogId { get; set; }
        [Required]
        public Guid FreelancerId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
    }
}
