using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public Guid OrderId { get; set; }
        public virtual OrderPostModel Order { get; set; }
    }
}
