using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using System.Net;

namespace Ordering.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        #region constructor
        private readonly IMediator mediator;
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        #endregion
        #region Get All Orders
        [HttpGet("{username}", Name = "GetOrders")]
        [ProducesResponseType(typeof(IEnumerable<OrderDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrderByUsername(string username)
        {
            var query = new GetOrderListQuery(username);
            var orders = await mediator.Send(query);
            return Ok(orders);
        }
        #endregion
        #region Checkout Order
        [HttpPost(Name = "CheckoutOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
        #endregion
        #region update order
        [HttpPost(Name = "UpdateOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
        { await mediator.Send(command); return NoContent(); }
        #endregion
        #region delete order
        [HttpDelete("{id}", Name = "DeleteOrder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteOrder(int id)
        { await mediator.Send(new DeleteOrderCommand { Id = id }); return NoContent(); }
        #endregion
    }
}
