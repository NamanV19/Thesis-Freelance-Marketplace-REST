using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.Context;
using OrderService.Data.Entities;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ViewModels;
using OrderService.Helper;

namespace OrderService.Controllers
{
    [ApiController, Route("api/v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        public readonly DatabaseContext _context;

        public PaymentController(DatabaseContext context) => _context = context;

        [HttpGet]
        public ActionResult<IEnumerable<PaymentViewModel>> GetPayments() =>
            _context.Payments.Select(EntityModelConverter.ConvertPaymentEntityToPaymentViewModel).ToList();

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentViewModel>> GetPayment(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) { return NotFound($"A payment with an id of {id} does not exist"); }
            return EntityModelConverter.ConvertPaymentEntityToPaymentViewModel(payment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentPostModel>> PutPayment(Guid id, PaymentPostModel payment)
        {
            // if (id != order.Id) { return BadRequest($"Incorrect id: {id} specified for Order. "); }
            var paymentEntity = await _context.Payments.FindAsync(id);
            if (paymentEntity == null) { return NotFound(); }
            else
            {
                paymentEntity.PaymentMethod = payment.PaymentMethod;
                paymentEntity.Price = payment.Price;
                paymentEntity.TransactionDate = payment.TransactionDate;
                paymentEntity.OrderId = payment.OrderId;

                _context.Entry(paymentEntity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return Ok(EntityModelConverter.ConvertPaymentEntityToPaymentViewModel(paymentEntity));
        }

        [HttpPost]
        public async Task<ActionResult<PaymentPostModel>> PostPayment(PaymentPostModel payment)
        {
            var paymentEntity = EntityModelConverter.ConvertPaymentPostModelToPaymentEntity(payment);

            _context.Payments.Add(paymentEntity);
            await _context.SaveChangesAsync();
            return CreatedAtAction("PostPayment", new { id = paymentEntity.Id }, EntityModelConverter.ConvertPaymentEntityToPaymentViewModel(paymentEntity));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentViewModel>> DeletePayment(Guid id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) { return NotFound(); }
            _context.Payments.Remove(payment);

            try { await _context.SaveChangesAsync(); }
            catch (Exception exception)
            {
                return BadRequest($"Exception deleting Payment: {id} Exception: {exception.Message} {exception.InnerException?.Message}");
            }

            return EntityModelConverter.ConvertPaymentEntityToPaymentViewModel(payment);
        }
    }
}
