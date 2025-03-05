using Microsoft.AspNetCore.Mvc;
using OrderMS.Models;
using OrderMS.Data;
using Microsoft.EntityFrameworkCore;

namespace OrderMS.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderContext _ctx;

        public OrderController(OrderContext ctx)
        {
            this._ctx = ctx;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await this._ctx.Orders.ToListAsync<Order>();
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            await this._ctx.Orders.AddAsync(order);
            await this._ctx.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetOrders),
                new { id = order.Id },
                order
            );
        }
    }
}