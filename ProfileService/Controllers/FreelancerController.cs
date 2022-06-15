using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using Common.PostModels;
using ProfileService.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.NavModels;

namespace ProfileService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class FreelancerController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public FreelancerController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<FreelancerViewModel>> GetFreelancers() =>
            _context.Freelancers.Select(EntityModelConverter.ConvertFreelancerEntityToFreelancerViewModel).ToList();

        [HttpGet("Nav")]
        public ActionResult<IEnumerable<FreelancerNavModel>> GetNavFreelancers() =>
           _context.Freelancers.Select(EntityModelConverter.ConvertFreelancerEntityToFreelancerNavModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<FreelancerViewModel>> GetFreelancer(Guid id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null) { return NotFound($"A freelancer with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertFreelancerEntityToFreelancerViewModel(freelancer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FreelancerPostModel>> PutFreelancer(Guid id, FreelancerPostModel freelancer)
        {
            // if (id != freelancer.Id) { return BadRequest($"Incorrect id: {id} specified for Freelancer. "); }
            var freelancerEntity = await _context.Freelancers.FindAsync(id);
            if (freelancerEntity == null) { return NotFound(); }
            else
            {
                freelancerEntity.FirstName = freelancer.FirstName;
                freelancerEntity.LastName = freelancer.LastName;
                freelancerEntity.Email = freelancer.Email;
                freelancerEntity.TelephoneNumber = freelancer.TelephoneNumber;
                freelancerEntity.Location = freelancer.Location;
                freelancerEntity.Type = freelancer.Type;
                freelancerEntity.Password = freelancer.Password;
                freelancerEntity.CV = freelancer.CV;
                freelancerEntity.LinkToPortfolio = freelancer.LinkToPortfolio;
                freelancerEntity.EducationalInstitution = freelancer.EducationalInstitution;
                freelancerEntity.YearsOfExperience = freelancer.YearsOfExperience;

                _context.Entry(freelancerEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertFreelancerEntityToFreelancerViewModel(freelancerEntity));
        }

        [HttpPost]
        public async Task<ActionResult<FreelancerPostModel>> PostFreelancer(FreelancerPostModel freelancer)
        {
            var freelancerEntity = EntityModelConverter.ConvertFreelancerPostModelToFreelancerEntity(freelancer);

            _context.Freelancers.Add(freelancerEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostFreelancer", new { id = freelancerEntity.Id }, EntityModelConverter.ConvertFreelancerEntityToFreelancerViewModel(freelancerEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<FreelancerViewModel>> DeleteFreelancer(Guid id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null) { return NotFound(); }
            _context.Freelancers.Remove(freelancer);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Freelancer: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertFreelancerEntityToFreelancerViewModel(freelancer);
        }
    }
}
