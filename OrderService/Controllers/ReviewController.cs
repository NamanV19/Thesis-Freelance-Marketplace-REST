using Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Helper;

namespace OrderService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class ReviewController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public ReviewController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<ReviewViewModel>> GetReviews() =>
            _context.Reviews.Select(EntityModelConverter.ConvertReviewEntityToReviewViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewViewModel>> GetReview(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) { return NotFound($"A review with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertReviewEntityToReviewViewModel(review);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReviewPostModel>> PutReview(Guid id, ReviewPostModel review)
        {
            // if (id != order.Id) { return BadRequest($"Incorrect id: {id} specified for Order. "); }
            var reviewEntity = await _context.Reviews.FindAsync(id);
            if (reviewEntity == null) { return NotFound(); }
            else
            {
                reviewEntity.Stars = review.Stars;
                reviewEntity.Comment = review.Comment;
                reviewEntity.OrderId = review.OrderId;

                _context.Entry(reviewEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertReviewEntityToReviewViewModel(reviewEntity));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewPostModel>> PostReview(ReviewPostModel review)
        {
            var reviewEntity = EntityModelConverter.ConvertReviewPostModelToReviewEntity(review);

            _context.Reviews.Add(reviewEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostReview", new { id = reviewEntity.Id }, EntityModelConverter.ConvertReviewEntityToReviewViewModel(reviewEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ReviewViewModel>> DeleteReview(Guid id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) { return NotFound(); }
            _context.Reviews.Remove(review);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Review: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertReviewEntityToReviewViewModel(review);
        }
    }
}
