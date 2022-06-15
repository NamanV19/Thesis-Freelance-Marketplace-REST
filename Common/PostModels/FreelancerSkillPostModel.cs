using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class FreelancerSkillPostModel
    {
        [Required]
        public Guid FreelancerId { get; set; }
        [Required]
        public Guid SkillId { get; set; }
    }
}
