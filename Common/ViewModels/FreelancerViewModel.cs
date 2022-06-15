using Common.Helper;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class FreelancerViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }
        public string CV { get; set; }
        public string LinkToPortfolio { get; set; }
        public string EducationalInstitution { get; set; }
        public string YearsOfExperience { get; set; }
        public virtual IEnumerable<FreelancerSkillPostModel> FreelancerSkills { get; set; }
    }
}
