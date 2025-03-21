using eCommerce.OrderMicroservice.Businesslogiclayer.ServiceContact;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace order_microservice.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderControllers : ControllerBase
    {
        private readonly IorderService _ordersService;
        public OrderControllers(IorderService orderservice)
        {
            _ordersService = orderservice;
        }

        // GET: /api/Orders
        [HttpGet("GetOrders")]
        public async Task<IEnumerable<OrderResponse>> Get()
        {
            List<OrderResponse> orders = await _ordersService.GetOrders();
            return orders;
        }

        // GET: /api/Orders/search/orderid/{orderID}
        [HttpGet("search/orderid/{orderID}")]
        public async Task<ActionResult<OrderResponse>> GetOrderByOrderID(Guid orderID)
        {
            OrderResponse order = await _ordersService.GetOrderByCondition(o => o.OrderID == orderID);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // GET: /api/Orders/search/productid/{productID}
        [HttpGet("search/productid/{productID}")]
        public async Task<IEnumerable<OrderResponse>> GetOrdersByProductID(Guid productID)
        {
            List<OrderResponse> orders = await _ordersService.GetOrdersByCondition(
                o => o.OrderItems.Any(oi => oi.ProductID == productID));

            return orders;
        }

        // GET: /api/Orders/search/orderDate/{orderDate}
        [HttpGet("/search/orderDate/{orderDate}")]
        public async Task<IEnumerable<OrderResponse>> GetOrdersByOrderDate(DateTime orderDate)
        {
            List<OrderResponse> orders = await _ordersService.GetOrdersByCondition(
                o => o.OrderDate.Date == orderDate.Date);

            return orders;
        }

        // POST api/Orders
        [HttpPost]
        public async Task<IActionResult> Post(OrderAddRequest orderAddRequest)
        {
            if (orderAddRequest == null)
            {
                return BadRequest("Invalid order data");
            }

            OrderResponse orderResponse = await _ordersService.AddOrder(orderAddRequest);

            if (orderResponse == null)
            {
                return Problem("Error in adding product");
            }

            return Created($"api/Orders/search/orderid/{orderResponse.OrderID}", orderResponse);
        }

        // PUT api/Orders/{orderID}
        [HttpPut("{orderID}")]
        public async Task<IActionResult> Put(Guid orderID, OrderUpdateRequest orderUpdateRequest)
        {
            if (orderUpdateRequest == null)
            {
                return BadRequest("Invalid order data");
            }

            if (orderID != orderUpdateRequest.OrderID)
            {
                return BadRequest("OrderID in the URL doesn't match with the OrderID in the Request body");
            }

            OrderResponse orderResponse = await _ordersService.UpdateOrder(orderUpdateRequest);

            if (orderResponse == null)
            {
                return Problem("Error in updating product");
            }

            return Ok(orderResponse);
        }

        // DELETE api/Orders/{orderID}
        [HttpDelete("{orderID}")]
        public async Task<IActionResult> Delete(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                return BadRequest("Invalid order ID");
            }

            bool isDeleted = await _ordersService.DeleteOrder(orderID);

            if (!isDeleted)
            {
                return Problem("Error in deleting product");
            }

            return Ok(isDeleted);
        }
    }
}
