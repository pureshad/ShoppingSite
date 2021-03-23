using ShoppingSite.Models.Entitys;
using ShoppingSite.RepositoryInfra;
using ShoppingSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ShoppingSite.Infrastructure.Services
{
    public class HomeService : IHomePage
    {
        private readonly IRepository<Products> _productsRepository;
        private readonly IRepository<CategoryType> _categoryTypeRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;

        public HomeService
            (
                IRepository<Products> productsRepository,
                IRepository<CategoryType> categoryTypeRepository,
                IRepository<ShoppingCart> shoppingCartRepository
            )
        {
            _productsRepository = productsRepository;
            _categoryTypeRepository = categoryTypeRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void AddProductToCart(int? id)
        {
            throw new NotImplementedException();
        }

        public ProductsViewModel CreateProduct()
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int? id)
        {
            throw new NotImplementedException();
        }

        public ProductsViewModel EditProduct(int? id)
        {
            throw new NotImplementedException();
        }

        public Products GetPoroductDetails(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Products> GetProsuctsHomePage()
        {
            var query = (from item in
                (from p in _productsRepository.Table
                 join ct in _categoryTypeRepository.Table on p.CategoryTypeId equals ct.Id
                 group new
                 {
                     p.Name,
                     p.Id
                 }
                 by new
                 {
                     p
                 })
                         select new Products()
                         {
                             //item
                             Brand = item.Key.p.Brand,
                             CategoryType = item.Key.p.CategoryType,
                             CategoryTypeId = item.Key.p.CategoryTypeId,
                             Description = item.Key.p.Description,
                             CreatedTime = item.Key.p.CreatedTime,
                             Id = item.Key.p.Id,
                             DisplayType = item.Key.p.DisplayType,
                             Image = item.Key.p.Image,
                             IsAvailableInStock = item.Key.p.IsAvailableInStock,
                             Name = item.Key.p.Name,
                             NumberAvailable = item.Key.p.NumberAvailable,
                             Price = item.Key.p.Price,
                             StrapQuality = item.Key.p.StrapQuality
                         }).ToList();

            return query;
        }

        public void SaveProduct(Products product)
        {
            throw new NotImplementedException();
        }
    }
}