using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data.Context;
using ProfileService.Data.Entities;
using Common.PostModels;
using ProfileService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class SkillController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public SkillController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<SkillViewModel>> GetSkills() =>
            _context.Skills.Select(EntityModelConverter.ConvertSkillEntityToSkillViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<SkillViewModel>> GetSkill(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) { return NotFound($"A skill with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertSkillEntityToSkillViewModel(skill);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SkillPostModel>> PutSkill(Guid id, SkillPostModel skill)
        {
            var skillEntity = await _context.Skills.FindAsync(id);
            if (skillEntity == null) { return NotFound(); }
            else
            {
                skillEntity.SkillName = skill.SkillName;

                _context.Entry(skillEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertSkillEntityToSkillViewModel(skillEntity));
        }

        [HttpPost]
        public async Task<ActionResult<SkillPostModel>> PostSkill(SkillPostModel skill)
        {
            var skillEntity = EntityModelConverter.ConvertSkillPostModelToSkillEntity(skill);

            _context.Skills.Add(skillEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostSkill", new { id = skillEntity.Id }, EntityModelConverter.ConvertSkillEntityToSkillViewModel(skillEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillViewModel>> DeleteSkill(Guid id)
        {
            var skill = await _context.Skills.FindAsync(id);
            if (skill == null) { return NotFound(); }
            _context.Skills.Remove(skill);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Skill: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertSkillEntityToSkillViewModel(skill);
        }
    }
}
