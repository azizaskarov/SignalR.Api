using Api.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalarLesson.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IHubContext<ValuesHub> _hubContext;

    public ValuesController(IHubContext<ValuesHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpGet]
    public List<int> GetValues()
    {
        return Data.Values;
    }

    [HttpPost]
    public async Task Add(int a)
    {
        Data.Values.Add(a);

        await _hubContext.Clients.All.SendAsync("NewValue", a);
    }
}


public static class Data
{
    public static List<int> Values = new List<int>() { 1, 2, 3 };
}
