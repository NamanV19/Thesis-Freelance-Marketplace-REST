using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels;
using ProfileService.Data;
using ProfileService.Data.Entities;
using Common.PostModels;
using Common.NavModels;

namespace ProfileService.Helper
{
    public class EntityModelConverter
    {
        public static BuyerViewModel ConvertBuyerEntityToBuyerViewModel(Buyer theBuyer)
        =>  theBuyer == null ? new BuyerViewModel() : new BuyerViewModel
        {
            Id = theBuyer.Id,
            FirstName = theBuyer.FirstName,
            LastName = theBuyer.LastName,
            Email = theBuyer.Email,
            TelephoneNumber = theBuyer.TelephoneNumber,
            Location = theBuyer.Location,
            Type = theBuyer.Type,
            Password = theBuyer.Password,
        };

        public static BuyerPostModel ConvertBuyerEntityToBuyerPostModel(Buyer theBuyer)
        => theBuyer == null ? new BuyerPostModel() : new BuyerPostModel
        {
            FirstName = theBuyer.FirstName,
            LastName = theBuyer.LastName,
            Email = theBuyer.Email,
            TelephoneNumber = theBuyer.TelephoneNumber,
            Location = theBuyer.Location,
            Type = theBuyer.Type,
            Password = theBuyer.Password,
        };

        public static Buyer ConvertBuyerPostModelToBuyerEntity(BuyerPostModel theBuyerModel)
        => theBuyerModel == null ? new Buyer() : new Buyer
        {
            FirstName = theBuyerModel.FirstName,
            LastName = theBuyerModel.LastName,
            Email = theBuyerModel.Email,
            TelephoneNumber = theBuyerModel.TelephoneNumber,
            Location = theBuyerModel.Location,
            Type = theBuyerModel.Type,
            Password = theBuyerModel.Password,
        };

        public static FreelancerViewModel ConvertFreelancerEntityToFreelancerViewModel(Freelancer theFreelancer)
        => theFreelancer == null ? new FreelancerViewModel() : new FreelancerViewModel
        {
            Id = theFreelancer.Id,
            FirstName = theFreelancer.FirstName,
            LastName = theFreelancer.LastName,
            Email = theFreelancer.Email,
            TelephoneNumber = theFreelancer.TelephoneNumber,
            Location = theFreelancer.Location,
            Type = theFreelancer.Type,
            Password = theFreelancer.Password,
            CV = theFreelancer.CV,
            LinkToPortfolio = theFreelancer.LinkToPortfolio,
            EducationalInstitution = theFreelancer.EducationalInstitution,
            YearsOfExperience = theFreelancer.YearsOfExperience,
            FreelancerSkills = theFreelancer.FreelancerSkills?.Select(ConvertFreelancerSkillEntityToFreelancerSkillPostModel).ToList()
        };

        public static FreelancerPostModel ConvertFreelancerEntityToFreelancerPostModel(Freelancer theFreelancer)
        => theFreelancer == null ? new FreelancerPostModel() : new FreelancerPostModel
        {
            FirstName = theFreelancer.FirstName,
            LastName = theFreelancer.LastName,
            Email = theFreelancer.Email,
            TelephoneNumber = theFreelancer.TelephoneNumber,
            Location = theFreelancer.Location,
            Type = theFreelancer.Type,
            Password = theFreelancer.Password,
            CV = theFreelancer.CV,
            LinkToPortfolio = theFreelancer.LinkToPortfolio,
            EducationalInstitution = theFreelancer.EducationalInstitution,
            YearsOfExperience = theFreelancer.YearsOfExperience
        };

        public static FreelancerNavModel ConvertFreelancerEntityToFreelancerNavModel(Freelancer theFreelancer)
        => theFreelancer == null ? new FreelancerNavModel() : new FreelancerNavModel
        {
            Id = theFreelancer.Id,
            FirstName = theFreelancer.FirstName,
            LastName = theFreelancer.LastName,
            Email = theFreelancer.Email,
            TelephoneNumber = theFreelancer.TelephoneNumber,
            Location = theFreelancer.Location,
            CV = theFreelancer.CV,
            LinkToPortfolio = theFreelancer.LinkToPortfolio,
            EducationalInstitution = theFreelancer.EducationalInstitution,
            YearsOfExperience = theFreelancer.YearsOfExperience
        };

        public static Freelancer ConvertFreelancerPostModelToFreelancerEntity(FreelancerPostModel theFreelancerModel)
        => theFreelancerModel == null ? new Freelancer() : new Freelancer
        {
            FirstName = theFreelancerModel.FirstName,
            LastName = theFreelancerModel.LastName,
            Email = theFreelancerModel.Email,
            TelephoneNumber = theFreelancerModel.TelephoneNumber,
            Location = theFreelancerModel.Location,
            Type = theFreelancerModel.Type,
            Password = theFreelancerModel.Password,
            CV = theFreelancerModel.CV,
            LinkToPortfolio = theFreelancerModel.LinkToPortfolio,
            EducationalInstitution = theFreelancerModel.EducationalInstitution,
            YearsOfExperience = theFreelancerModel.YearsOfExperience
        };

        public static SkillViewModel ConvertSkillEntityToSkillViewModel(Skill theSkill)
        => theSkill == null ? new SkillViewModel() : new SkillViewModel
        {
            Id = theSkill.Id,
            SkillName = theSkill.SkillName,
            FreelancerSkills = theSkill.FreelancerSkills?.Select(ConvertFreelancerSkillEntityToFreelancerSkillPostModel).ToList()
        };

        public static SkillPostModel ConvertSkillEntityToSkillPostModel(Skill theSkill)
        => theSkill == null ? new SkillPostModel() : new SkillPostModel
        {
            SkillName = theSkill.SkillName
        };

        public static Skill ConvertSkillPostModelToSkillEntity(SkillPostModel theSkillModel)
        => theSkillModel == null ? new Skill() : new Skill
        {
            SkillName = theSkillModel.SkillName
        };

        public static FreelancerSkillViewModel ConvertFreelancerSkillEntityToFreelancerSkillViewModel(FreelancerSkill theFreelancerSkill)
        => theFreelancerSkill == null ? new FreelancerSkillViewModel() : new FreelancerSkillViewModel
        {
            Id = theFreelancerSkill.Id,
            FreelancerId = theFreelancerSkill.FreelancerId,
            SkillId = theFreelancerSkill.SkillId,
            Freelancer = ConvertFreelancerEntityToFreelancerPostModel(theFreelancerSkill.Freelancer),
            Skill = ConvertSkillEntityToSkillPostModel(theFreelancerSkill.Skill)
        };

        public static FreelancerSkillPostModel ConvertFreelancerSkillEntityToFreelancerSkillPostModel(FreelancerSkill theFreelancerSkill)
        => theFreelancerSkill == null ? new FreelancerSkillPostModel() : new FreelancerSkillPostModel
        {
            FreelancerId = theFreelancerSkill.FreelancerId,
            SkillId = theFreelancerSkill.SkillId,
        };

        public static FreelancerSkill ConvertFreelancerSkillPostModelToFreelancerSkillEntity(FreelancerSkillPostModel theFreelancerSkillModel)
        => theFreelancerSkillModel == null ? new FreelancerSkill() : new FreelancerSkill
        {
            FreelancerId = theFreelancerSkillModel.FreelancerId,
            SkillId = theFreelancerSkillModel.SkillId,
        };
    }
}
