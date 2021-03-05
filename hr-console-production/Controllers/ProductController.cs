using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Serilog;
using System;

namespace hr_console_production.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Log.Logger.Information("Get all products");

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
                    weight = p.Weight
                }).ToArray();

                return Ok(products);
            }               
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using var ctx = new Models.awvmsqldbContext();
            var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);

            if(product == null)
            {
                Log.Logger.Information($"{id} not found.");
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
                weight = product.Weight
            };

            return Ok(mapped);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using var ctx = new Models.awvmsqldbContext();
            var product = ctx.Products.SingleOrDefault(p => p.ProductId == id);

            if(product == null)
            {
                Log.Logger.Information($"{id} not found.");
                return NotFound();
            }

            ctx.Products.Remove(product);
            ctx.SaveChanges();

            return Ok();
        }

        [HttpPut]
        public void Put(int id)
        {
            try
            {
                throw new NotImplementedException("Expected exception.");
            }
            catch(Exception ex)
            {
                Log.Logger.Error("Exception: " + ex.Message);
            }
        }
    }
}
