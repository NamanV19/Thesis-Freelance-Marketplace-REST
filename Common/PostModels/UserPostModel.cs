using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class UserPostModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Invalid Email ")]
        public string Email { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Invalid phone number length. Kindly input the country code as well.")] // Including the country code
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone number must be numeric.")]
        public string TelephoneNumber { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Location { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$")]
        public string Type { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
