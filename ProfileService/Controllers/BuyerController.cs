using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfileService.Helper;

namespace ProfileService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class BuyerController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public BuyerController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<BuyerViewModel>> GetBuyers() =>
            _context.Buyers.Select(EntityModelConverter.ConvertBuyerEntityToBuyerViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<BuyerViewModel>> GetBuyer(Guid id)
        {
            var buyerEntity = await _context.Buyers.FindAsync(id);
            if (buyerEntity == null) { return NotFound($"A buyer with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertBuyerEntityToBuyerViewModel(buyerEntity);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BuyerPostModel>> PutBuyer(Guid id, BuyerPostModel buyer)
        {
            // if (id != buyer.Id) { return BadRequest($"Incorrect id: {id} specified for Buyer. "); }
            var buyerEntity = await _context.Buyers.FindAsync(id);
            if (buyerEntity == null) { return NotFound(); }
            else
            {
                buyerEntity.FirstName = buyer.FirstName;
                buyerEntity.LastName = buyer.LastName;
                buyerEntity.Email = buyer.Email;
                buyerEntity.TelephoneNumber = buyer.TelephoneNumber;
                buyerEntity.Location = buyer.Location;
                buyerEntity.Type = buyer.Type;
                buyerEntity.Password = buyer.Password;

                _context.Entry(buyerEntity).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound("Cannot update Buyer's information");
            }

            return Ok(buyer);
        }

        [HttpPost]
        public async Task<ActionResult<BuyerPostModel>> PostBuyer(BuyerPostModel buyer)
        {
            var buyerEntity = EntityModelConverter.ConvertBuyerPostModelToBuyerEntity(buyer);

            _context.Buyers.Add(buyerEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostBuyer", new { id = buyerEntity.Id }, EntityModelConverter.ConvertBuyerEntityToBuyerViewModel(buyerEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BuyerViewModel>> DeleteBuyer(Guid id)
        {
            var buyerEntity = await _context.Buyers.FindAsync(id);
            if (buyerEntity == null) { return NotFound(); }
            _context.Buyers.Remove(buyerEntity);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Buyer: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertBuyerEntityToBuyerViewModel(buyerEntity);
        }
    }
}
