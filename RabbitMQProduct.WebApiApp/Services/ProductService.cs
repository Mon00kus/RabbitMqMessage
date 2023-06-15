using Microsoft.EntityFrameworkCore;
using RabbitMQProduct.WebApiApp.Data;
using RabbitMQProduct.WebApiApp.Models;

namespace RabbitMQProduct.WebApiApp.Services
{
    public class ProductService : IProductService
    {
        public AppDbContext _appDbContext { get; }
        public ProductService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Product> GetProductList()
        {
            return _appDbContext.Products.ToList();
        }

        public IQueryable<Product> GetProducts()
        {
            return _appDbContext.Products;
        }

        public Product GetProductById(int id)
        {
            return _appDbContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
        }
        public Product AddProduct(Product product)
        {
            var result  =  _appDbContext.Products.Add(product);
            _appDbContext.SaveChanges();
            return result.Entity;
        }

        public Product UpdateProduct(Product product)
        {
            var result = _appDbContext.Products.Update(product);
            _appDbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteProduct(int Id)
        {
            var filteredData = _appDbContext.Products.Where(x => x.ProductId == Id).FirstOrDefault();
            var result = _appDbContext.Remove(filteredData);
            _appDbContext.SaveChanges();
            return result != null ? true : false;
        }
    }
}
