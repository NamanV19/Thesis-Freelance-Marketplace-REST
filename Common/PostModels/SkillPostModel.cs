using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.PostModels
{
    public class SkillPostModel
    {
        [Required]
        public string SkillName { get; set; }
    }
}
