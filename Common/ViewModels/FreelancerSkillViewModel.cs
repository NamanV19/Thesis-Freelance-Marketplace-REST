using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class FreelancerSkillViewModel
    {
        public Guid Id { get; set; }
        public Guid FreelancerId { get; set; }
        public Guid SkillId { get; set; }
        public virtual FreelancerPostModel Freelancer { get; set; }
        public virtual SkillPostModel Skill { get; set; }
    }
}
