using ShoppingSite.Models.Entitys;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSite.Infrastructure
{
    public interface IHomePage
    {
        List<Products> GetProsuctsHomePage();
        Products GetPoroductDetails(int? id);
        ProductsViewModel CreateProduct();
        void AddProductToCart(int? id);
        void SaveProduct(Products product);
        ProductsViewModel EditProduct(int? id);
        void DeleteProduct(int? id);

    }
}
