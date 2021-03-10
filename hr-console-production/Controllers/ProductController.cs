using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Serilog;
using System;
using hr_console_production.Models;
using Newtonsoft.Json;

namespace hr_console_production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger _logger;

        public ProductController(ILogger logger)
        {
            this._logger = logger.ForContext<ProductController>();
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.Information("Get all products");

            using (var ctx = new Models.awvmsqldbContext())
            {
                var products = ctx.Products.Select(p => new
                {
                    productId = p.ProductId,
                    name = p.Name,
                    productNumber = p.ProductNumber,
                    sellEndDate = p.SellEndDate,
                    sellStartDate = p.SellStartDate,
                    listPrice = p.ListPrice,
                    weight = p.Weight,
                    size = p.Size
                }).ToArray();

                return Ok(products);
            }               
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.Information($"Get product by id {id}.");

            using var ctx = new Models.awvmsqldbContext();
            var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);

            if(product == null)
            {
                _logger.Information($"Product with id {id} not found.");
                return NotFound();
            }

            var mapped = new
            {
                productId = product.ProductId,
                name = product.Name,
                productNumber = product.ProductNumber,
                sellEndDate = product.SellEndDate,
                sellStartDate = product.SellStartDate,
                listPrice = product.ListPrice,
                weight = product.Weight,
                size = product.Size
            };

            return Ok(mapped);
        }

        [HttpGet("GetByName/{name}")]
        public IActionResult Get(string name)
        {
            _logger.Information($"Get product by name {name}.");

            using var ctx = new Models.awvmsqldbContext();
            var product = ctx.Products.SingleOrDefault(p => p.Name == name);

            if (product == null)
            {
                _logger.Information($"Product with name {name} not found.");
                return NotFound();
            }

            var mapped = new
            {
                productId = product.ProductId,
                name = product.Name,
                productNumber = product.ProductNumber,
                sellEndDate = product.SellEndDate,
                sellStartDate = product.SellStartDate,
                listPrice = product.ListPrice,
                weight = product.Weight,
                size = product.Size
            };

            return Ok(mapped);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.Information($"Delete product by id {id}.");

            using var ctx = new Models.awvmsqldbContext();
            var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);

            if(product == null)
            {
                _logger.Information($"{id} not found.");
                return NotFound();
            }

            ctx.Products.Remove(product);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public void Put(int id, ProductDto product)
        {
            _logger.Information($"Update product by id {id}.");

            using var ctx = new Models.awvmsqldbContext();
            var saved = ctx.Products.SingleOrDefault(p => p.ProductId == id);
            saved.Name = product.Name;
            saved.Size = product.Size;

            ctx.SaveChanges();
        }

        [HttpPost]
        public void Post([FromBody] ProductDto value)
        {
            _logger.Information($"Post product {JsonConvert.SerializeObject(value)}.");

            using var ctx = new awvmsqldbContext();
            var product = new Product
            {
                Name = value.Name,
                Size = value.Size,
                SellStartDate = DateTime.Now,
                SellEndDate = DateTime.Now,
                ProductNumber = "1",
                ReorderPoint = 750,
                SafetyStockLevel = 1000
            };
            ctx.Add(product);
            ctx.SaveChanges();
        }

        [HttpGet("GetException")]
        public IActionResult GetException()
        {
            _logger.Information("Throw exception.");
            throw new NotImplementedException();
        }
    }
}
