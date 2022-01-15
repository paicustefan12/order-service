using OrderService.BLL.Interfaces;
using OrderService.BLL.Models;
using OrderService.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager orderManager;

        public OrderController(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] Order order)
        {
            await orderManager.CreateOrder(order);
            return NoContent();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] Order order)
        {
            await orderManager.UpdateOrder(order);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await orderManager.DeleteOrder(id);
            return NoContent();
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelAsync([FromRoute] int id)
        {
            var response = await orderManager.CancelOrder(id);
            return response == true ? Ok() : BadRequest();
        }

        [HttpPost("success/{id}")]
        public async Task<IActionResult> SuccessAsync([FromRoute] int id)
        {
            var response = await orderManager.SuccessOrder(id);
            return Ok();
        }

        [HttpPost("fail/{id}")]
        [Authorize("Manager")]
        public async Task<IActionResult> FailAsync([FromRoute] int id)
        {
            var response = await orderManager.FailOrder(id);
            return response == true ? Ok() : BadRequest();
        }
    }
}
