using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Helper;

namespace ProfileService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FreelancerSkillController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public FreelancerSkillController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<FreelancerSkillViewModel>> GetFreelancerSkills() =>
            _context.FreelancerSkills.Select(EntityModelConverter.ConvertFreelancerSkillEntityToFreelancerSkillViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<FreelancerSkillViewModel>> GetFreelancerSkill(Guid id)
        {
            var freelancerSkill = await _context.FreelancerSkills.FindAsync(id);
            if (freelancerSkill == null) { return NotFound($"A freelancer skill with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertFreelancerSkillEntityToFreelancerSkillViewModel(freelancerSkill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FreelancerSkillPostModel>> PutFreelancerSkill(Guid id, FreelancerSkillPostModel freelancerSkill)
        {
            var freelancerSkillEntity = await _context.FreelancerSkills.FindAsync(id);
            if (freelancerSkillEntity == null) { return NotFound(); }
            else
            {
                freelancerSkillEntity.FreelancerId = freelancerSkill.FreelancerId;
                freelancerSkillEntity.SkillId = freelancerSkill.SkillId;

                _context.Entry(freelancerSkillEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertFreelancerSkillEntityToFreelancerSkillViewModel(freelancerSkillEntity));
        }

        [HttpPost]
        public async Task<ActionResult<FreelancerSkillPostModel>> PostFreelancerSkill(FreelancerSkillPostModel freelancerSkill)
        {
            var freelancerSkillEntity = EntityModelConverter.ConvertFreelancerSkillPostModelToFreelancerSkillEntity(freelancerSkill);

            _context.FreelancerSkills.Add(freelancerSkillEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostFreelancerSkill", new { id = freelancerSkillEntity.Id }, EntityModelConverter.ConvertFreelancerSkillEntityToFreelancerSkillViewModel(freelancerSkillEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FreelancerSkillViewModel>> DeleteFreelancerSkill(Guid id)
        {
            var freelancerSkill = await _context.FreelancerSkills.FindAsync(id);
            if (freelancerSkill == null) { return NotFound(); }
            _context.FreelancerSkills.Remove(freelancerSkill);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting FreelancerSkill: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertFreelancerSkillEntityToFreelancerSkillViewModel(freelancerSkill);
        }
    }
}
