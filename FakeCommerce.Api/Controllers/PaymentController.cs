﻿using FakeCommerce.Api.Services;
using FakeCommerce.Api.ViewModels.Basket;
using FakeCommerce.Api.ViewModels.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeCommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IServiceManager _service;

        public PaymentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpPost("create-checkout-session"), Authorize]
        public ActionResult Create([FromBody] List<BasketItemDto> items)
        {
            var session = _service.PaymentService.CreateCheckoutSession(items, User?.Identity?.Name);
            return Ok( new { session.Id, session.Url });
        }

        /// <summary>
        /// Stripe webhook
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FulfillOrder()
        {
            var response = await _service.PaymentService.FulfillOrder(Request);
            
            if (!response.Success)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}
