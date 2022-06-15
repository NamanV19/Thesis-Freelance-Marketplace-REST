using OrderService.Data.Entities;
using Common.ViewModels;
using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Helper
{
    public class EntityModelConverter
    {
        public static OrderViewModel ConvertOrderEntityToOrderViewModel(Order theOrder)
        => theOrder == null ? new OrderViewModel() : new OrderViewModel
        {
           Id = theOrder.Id,
           CatalogId = theOrder.CatalogId,
           FreelancerId = theOrder.FreelancerId,
           StartDate = theOrder.StartDate,
           EndDate = theOrder.EndDate,
           Payment = ConvertPaymentEntityToPaymentPostModel(theOrder.Payment),
           Review = ConvertReviewEntityToReviewPostModel(theOrder.Review),
        };

        public static OrderPostModel ConvertOrderEntityToOrderPostModel(Order theOrder)
        => theOrder == null ? new OrderPostModel() : new OrderPostModel
        {
            CatalogId = theOrder.CatalogId,
            FreelancerId = theOrder.FreelancerId,
            StartDate = theOrder.StartDate,
            EndDate = theOrder.EndDate,
        };

        public static Order ConvertOrderPostModelToOrderEntity(OrderPostModel theOrderModel)
        => theOrderModel == null ? new Order() : new Order
        {
            CatalogId = theOrderModel.CatalogId,
            FreelancerId = theOrderModel.FreelancerId,
            StartDate = theOrderModel.StartDate,
            EndDate = theOrderModel.EndDate,
        };

        public static PaymentViewModel ConvertPaymentEntityToPaymentViewModel(Payment thePayment)
        => thePayment == null ? new PaymentViewModel() : new PaymentViewModel
        {
            Id = thePayment.Id,
            PaymentMethod = thePayment.PaymentMethod,
            TransactionDate = thePayment.TransactionDate,
            Price = thePayment.Price,
            OrderId = thePayment.OrderId,
            Order = ConvertOrderEntityToOrderPostModel(thePayment.Order),
        };

        public static PaymentPostModel ConvertPaymentEntityToPaymentPostModel(Payment thePayment)
        => thePayment == null ? new PaymentPostModel() : new PaymentPostModel
        {
            PaymentMethod = thePayment.PaymentMethod,
            TransactionDate = thePayment.TransactionDate,
            Price = thePayment.Price,
            OrderId = thePayment.OrderId,
        };

        public static Payment ConvertPaymentPostModelToPaymentEntity(PaymentPostModel thePaymentModel)
        => thePaymentModel == null ? new Payment() : new Payment
        {
            PaymentMethod = thePaymentModel.PaymentMethod,
            TransactionDate = thePaymentModel.TransactionDate,
            Price = thePaymentModel.Price,
            OrderId = thePaymentModel.OrderId,
        };

        public static ReviewViewModel ConvertReviewEntityToReviewViewModel(Review theReview)
        => theReview == null ? new ReviewViewModel() : new ReviewViewModel
        {
            Id = theReview.Id,
            Stars = theReview.Stars,
            Comment = theReview.Comment,
            OrderId = theReview.OrderId,
            Order = ConvertOrderEntityToOrderPostModel(theReview.Order),
        };

        public static ReviewPostModel ConvertReviewEntityToReviewPostModel(Review theReview)
        => theReview == null ? new ReviewPostModel() : new ReviewPostModel
        {
            Stars = theReview.Stars,
            Comment = theReview.Comment,
            OrderId = theReview.OrderId,
        };

        public static Review ConvertReviewPostModelToReviewEntity(ReviewPostModel theReviewModel)
        => theReviewModel == null ? new Review() : new Review
        {
            Stars = theReviewModel.Stars,
            Comment = theReviewModel.Comment,
            OrderId = theReviewModel.OrderId,
        };
    }
}
