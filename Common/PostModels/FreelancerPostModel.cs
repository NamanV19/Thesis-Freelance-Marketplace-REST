using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class FreelancerPostModel : UserPostModel
    {
        [Required]
        public string CV { get; set; }

        [Required]
        public string LinkToPortfolio { get; set; }

        [Required]
        public string EducationalInstitution { get; set; }

        [Required]
        [MaxLength(2)]
        [MinLength(1)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Years of experience should be numeric and not exceed 99")]
        public string YearsOfExperience { get; set; }
    }
}
