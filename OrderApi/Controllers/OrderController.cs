using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderApi.Dtos;
using OrderApi.Models;
using OrderApi.Repositories;

namespace OrderApi.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : Controller
{
    private readonly EventRepositories _eventRepositories;
    private readonly UserRepositories _userRepositories;
    private readonly OrderRepositories _orderRepositories;

    public OrderController(
        EventRepositories eventRepositories,
        UserRepositories userRepositories,
        OrderRepositories orderRepositories)
    {
        _eventRepositories = eventRepositories;
        _userRepositories = userRepositories;
        _orderRepositories = orderRepositories;
    }

    [HttpPost]
    public IActionResult Post(OrderPostDto dto)
    {
        // validation
        var isUserExist = _userRepositories.Exist(dto.UserId);
        if (!isUserExist)
        {
            return BadRequest($"user id '{dto.UserId}' not exist");
        }
        var @event = _eventRepositories.Get(dto.EventId);
        if (@event == null)
        {
            return BadRequest($"event id '{dto.EventId}' not exist");
        }

        // check max participant limitation
        if (@event.MaxSeats != null)
        {
            var orders = _orderRepositories.GetAll();
            if (orders != null)
            {
                var count = orders.Where(x => x.EventId == @event.EventId).Count();
                if (count + 1 > @event.MaxSeats)
                {
                    return BadRequest($"sorry, event is fully ordered already");
                }
            }
        }

        var order = new Order()
        {
            UserId = dto.UserId,
            EventId = dto.EventId,
        };
        _orderRepositories.Add(order);

        return Ok();
    }
}
