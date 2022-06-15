using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class CatalogViewModel
    {
        public Guid Id { get; set; }
        public string TypeOfWork { get; set; } // => [Part time, Full time]
        public string TitleOfJob { get; set; }
        public string JobDescription { get; set; }
        public string JobCategory { get; set; } // => [Mobile development, Graphic design, Web development, Game development, Article writing]
        public string ScopeOfWork { get; set; } // => [Small, Medium, Large]
        public string EstimatedTime { get; set; } // => [<3 months, 3-6 months, >6 months]
        public decimal Budget { get; set; }
        public string Status { get; set; } // => [None, Under Progress, Done]
        public DateTime dateCreated { get; set; } = DateTime.Now;
        public Guid BuyerId { get; set; }
    }
}
