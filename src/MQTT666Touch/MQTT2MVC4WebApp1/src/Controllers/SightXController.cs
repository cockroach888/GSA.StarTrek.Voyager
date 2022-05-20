using Microsoft.AspNetCore.Mvc;

namespace MQTT2MVC4WebApp1.Controllers;

[Route("[controller]/[action]")]
public class SightXController : Controller
{
    private readonly ILogger<SightXController> _logger;

    public SightXController(ILogger<SightXController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Client()
    {
        _logger.LogDebug($"进入MQTT客户端测试页面。");

        return View();
    }
}
