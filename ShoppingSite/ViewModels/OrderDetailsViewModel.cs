using ShoppingSite.Models.Entitys;
using System.Collections.Generic;

namespace ShoppingSite.ViewModels
{
    public class OrderDetailsViewModel
    {
        public List<OrderDetailsViewModel> OrderDetailsViewModels { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}