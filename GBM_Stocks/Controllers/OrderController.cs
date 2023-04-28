using GBM_Stocks_Accounts_Core.Interfaces;
using GBM_Stocks_Accounts_Domain.Request;
using GBM_Stocks_Orders_Core.Interfaces;
using GBM_Stocks_Orders_Domain.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GBM_Stocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersService OrdersService;

        public OrderController(IOrdersService ordersService)
        {
            OrdersService = ordersService;
        }

        [HttpPost("Post")]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            var response = await OrdersService.CreateOrder(createOrderRequest, "Create order");
            return Ok(response);
        }
    }
}
