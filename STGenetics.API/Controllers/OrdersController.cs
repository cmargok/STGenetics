﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using STGenetics.Application.Models.Order;
using STGenetics.Application.Services;
using STGenetics.Application.Tools.Settings;
using STGenetics.Domain.Tools.ApiResponses;
using STGenetics.Domain.Tools;

namespace STGenetics.API.Controllers
{

    /// <summary>
    /// Orders Controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    // [Authorize]
    public class OrdersController : ControllerBase
    {

        private readonly IOrderService _orderService;
        
        /// <summary>
        /// Order Constructor
        /// </summary>
        /// <param name="orderService"></param>
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Create, process and saves a Order
        /// </summary>
        /// <param name="orderIn"></param>
        /// <returns>Order information: the order id and the total price for the order</returns>
        [ProducesResponseType(typeof(ApiResponse<OrderOut>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(Problem), 500)]
        [HttpPost]
        public async Task<IActionResult> ProcessTransaction(OrderIn orderIn) 
        {
            var orderResponse = await _orderService.ProcessOrder(orderIn);

            if (orderResponse.Item1.Id == 0)
            {
                var BadResponse = Tools.CreateResponse(orderResponse.Item2, Result.BadRequest, 0);
                return BadRequest(BadResponse);
            }

            var response = Tools.CreateResponse(orderResponse.Item1, Result.Success, 1);

            return Ok(response);

        }
    }
}