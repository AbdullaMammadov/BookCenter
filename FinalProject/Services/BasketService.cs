
// BasketService.cs
using FinalProject.DAL;
using FinalProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppDbContext _context;
        public BasketService(IHttpContextAccessor contextAccessor, AppDbContext context)
        {
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public void DecreaseBook(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var decreasedProduct = products.FirstOrDefault(p => p.Id == id&&p.IsBook==true);

            if (decreasedProduct != null)
            {
                if (decreasedProduct.BasketCount > 0)
                {
                    decreasedProduct.BasketCount--;

                    if (decreasedProduct.BasketCount == 0)
                    {
                        var productToRemove = products.FirstOrDefault(p => p.Id == id);
                        if (productToRemove != null)
                        {
                            products.Remove(productToRemove);
                        }
                    }


                    _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
                }
            }
        }

        public void DecreaseProduct(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var decreasedProduct = products.FirstOrDefault(p => p.Id == id && p.IsBook == false);

            if (decreasedProduct != null)
            {
                if (decreasedProduct.BasketCount > 0)
                {
                    decreasedProduct.BasketCount--;

                    if (decreasedProduct.BasketCount == 0)
                    {
                        var productToRemove = products.FirstOrDefault(p => p.Id == id);
                        if (productToRemove != null)
                        {
                            products.Remove(productToRemove);
                        }
                    }


                    _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
                }
            }
        }

        public void DeleteBook(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var deletedProduct = products.FirstOrDefault(p => p.Id == id&&p.IsBook==true);

            if (deletedProduct != null)
            {
                products.Remove(deletedProduct);
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }
        public void DeleteProduct(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var deletedProduct = products.FirstOrDefault(p => p.Id == id && p.IsBook == false);

            if (deletedProduct != null)
            {
                products.Remove(deletedProduct);
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }
        public List<BasketVM> GetBasket()
        {
            List<BasketVM> baskets = new();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];

            if (basket == null) return baskets;
            baskets = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            foreach (var basketProduct in baskets)
            {
                if (basketProduct.IsBook==true)
                {
                    var dbProduct = _context.Books.FirstOrDefault(p => p.Id == basketProduct.Id);
                    basketProduct.Name = dbProduct.Name;
                    basketProduct.Price = (dbProduct.Price);
                    basketProduct.DiscountPrice = (dbProduct.DiscountPrice);
                    basketProduct.ImageUrl = dbProduct.ImgUrl;
                }
                else
                {
                    var dbProduct = _context.Products.FirstOrDefault(p => p.Id == basketProduct.Id);
                    basketProduct.Name = dbProduct.Name;
                    basketProduct.Price = (double)(dbProduct.DiscountPrice ?? dbProduct.Price);
                    basketProduct.DiscountPrice = (dbProduct.DiscountPrice);
                    basketProduct.ImageUrl = dbProduct.ImgUrl;
                }
            }

            return baskets;

        }

        public int GetCount()
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket == null) return 0;
            return JsonConvert.DeserializeObject<List<BasketVM>>(basket).Count;

        }

        public void IncreaseBook(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var increasedProduct = products.FirstOrDefault(p => p.Id == id&&p.IsBook==true);

            if (increasedProduct != null)
            {
                increasedProduct.BasketCount++;
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }
        public void IncreaseProduct(int id)
        {
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            var increasedProduct = products.FirstOrDefault(p => p.Id == id && p.IsBook == false);

            if (increasedProduct != null)
            {
                increasedProduct.BasketCount++;
                _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
        }
    }
}