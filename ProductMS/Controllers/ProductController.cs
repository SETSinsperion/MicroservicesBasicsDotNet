using Microsoft.AspNetCore.Mvc;
using OrderMS.Models;
using OrderMS.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderMS.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _ctx;

        public ProductController(ProductContext ctx)
        {
            this._ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await this._ctx.Products.ToListAsync<Product>();
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await this._ctx.Products.AddAsync(product);
            await this._ctx.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProducts),
                new { id = product.Id },
                product
            );
        }
    }
}