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
    public class OrderController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public OrderController(DatabaseContext context) => _context = context;

        // Lazy Load
        [HttpGet]
        public ActionResult<IEnumerable<OrderViewModel>> GetOrders() =>
            _context.Orders.Select(EntityModelConverter.ConvertOrderEntityToOrderViewModel).ToList();

        // Eager Load
        /* [HttpGet]
        public ActionResult<IEnumerable<OrderViewModel>> GetOrders()
        {
            var orders = _context.Orders
                .Include(order => order.Payment)
                .Include(order => order.Review)
                .Select(EntityModelConverter.ConvertOrderEntityToOrderViewModel).ToList();

            return orders;
        } */

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) { return NotFound($"A order with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertOrderEntityToOrderViewModel(order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderPostModel>> PutOrder(Guid id, OrderPostModel order)
        {
            // if (id != order.Id) { return BadRequest($"Incorrect id: {id} specified for Order. "); }
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null) { return NotFound(); }
            else
            {
                orderEntity.CatalogId = order.CatalogId;
                orderEntity.FreelancerId = order.FreelancerId;
                orderEntity.StartDate = order.StartDate;
                orderEntity.EndDate = order.EndDate;

                _context.Entry(orderEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertOrderEntityToOrderViewModel(orderEntity));
        }

        [HttpPost]
        public async Task<ActionResult<OrderPostModel>> PostOrder(OrderPostModel order)
        {
            var orderEntity = EntityModelConverter.ConvertOrderPostModelToOrderEntity(order);

            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostOrder", new { id = orderEntity.Id }, EntityModelConverter.ConvertOrderEntityToOrderViewModel(orderEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderViewModel>> DeleteOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) { return NotFound(); }
            _context.Orders.Remove(order);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Order: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertOrderEntityToOrderViewModel(order);
        }
    }
}
