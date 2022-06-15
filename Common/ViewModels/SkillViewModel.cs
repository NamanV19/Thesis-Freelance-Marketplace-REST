using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class SkillViewModel
    {
        public Guid Id { get; set; }
        public string SkillName { get; set; }
        public virtual IEnumerable<FreelancerSkillPostModel> FreelancerSkills { get; set; }
    }
}
