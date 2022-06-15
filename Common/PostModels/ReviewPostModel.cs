using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class ReviewPostModel
    {
        [Required]
        [RegularExpression("^[1-5]", ErrorMessage = "Please input any number between 1 to 5")]
        public int Stars { get; set; }
        public string Comment { get; set; }
        [Required]
        public Guid OrderId { get; set; }
    }
}
