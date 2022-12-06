using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderApi.Models;
using OrderApi.Repositories;

namespace OrderApi.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : Controller
{
    
    private const string USER_DATA_PATH = @"Data\users.json";
    private readonly EventRepositories _eventRepositories;
    public EventController(EventRepositories eventRepositories)
    {
        _eventRepositories = eventRepositories;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var events = _eventRepositories.GetAll();
        return Ok(events);
    }

    //[HttpPost("order")]
    //public IActionResult Order(int id)
    //{
    //    var jsonString = System.IO.File.ReadAllText(EVENT_DATA_PATH);
    //    var events = JsonConvert.DeserializeObject<List<Event>>(jsonString);
    //    var @event = events.Where(x => x.EventId)
    //    return Ok(jsonString);
    //}
}
