using Common.PostModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ViewModels
{
    public class BuyerViewModel : UserViewModel
    {
        public List<CatalogViewModel> Catalogs { get; set; }
    }
}
