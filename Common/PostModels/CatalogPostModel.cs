using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class CatalogPostModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string TypeOfWork { get; set; }
        [Required]
        public string TitleOfJob { get; set; }
        [Required]
        public string JobDescription { get; set; }
        [Required]
        public string JobCategory { get; set; }
        [Required]
        public string ScopeOfWork { get; set; }
        [Required]
        public string EstimatedTime { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime dateCreated { get; set; } = DateTime.Now;
        [Required]
        public Guid BuyerId { get; set; }
    }
}
