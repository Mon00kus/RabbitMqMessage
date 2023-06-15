using Microsoft.AspNetCore.Mvc;
using RabbitMQProduct.WebApiApp.Models;
using RabbitMQProduct.WebApiApp.RabbitMQ;
using RabbitMQProduct.WebApiApp.Services;

namespace RabbitMQProduct.WebApiApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductService             _productService;
    private readonly IRabbitMQProducer           _rabbitMQProducer;
    public ProductsController(ILogger<ProductsController> logger,
        IProductService productService,
        IRabbitMQProducer rabbitMQProducer)
    {
        _logger = logger;
        _productService = productService;
        _rabbitMQProducer = rabbitMQProducer;
    }

    [HttpGet(Name = "productlist")]
    public IEnumerable<Product> ProductList()
    {
        var productList = _productService.GetProductList();
        return productList;
    }

    [HttpGet("getproductbyid")]
    public Product GetProductById(int Id)
    {
        return _productService.GetProductById(Id);
    }

    [HttpPost(Name = "addproduct")]
    public Product AddProduct(Product product)
    {
        var productData = _productService.AddProduct(product);

        //send the inserted product data to the queue and consumer will listening this data from queue

        _rabbitMQProducer.SendProductMessage<Product>(productData);

        return productData;
    }

    [HttpPut(Name = "updateproduct")]
    public Product UpdateProduct(Product product)
    {
        return _productService.UpdateProduct(product);
    }

    [HttpDelete(Name = "deleteproduct")]
    public bool DeleteProduct(int Id)
    {
        return _productService.DeleteProduct(Id);
    }
}
