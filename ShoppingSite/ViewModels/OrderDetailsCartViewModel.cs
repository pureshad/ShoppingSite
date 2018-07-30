using ShoppingSite.Models.Entitys;
using System.Collections.Generic;

namespace ShoppingSite.ViewModels
{
    public class OrderDetailsCartViewModel
    {
        public List<ShoppingCart> ShoppingCarts { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}