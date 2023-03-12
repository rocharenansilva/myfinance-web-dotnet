using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Models;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
    private readonly ILogger<TransacaoController> _logger;

    public TransacaoController(ILogger<TransacaoController> logger)
    {
      _logger = logger;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    public IActionResult Cadastro()
    {
      return View();
    }

    [HttpPost]
    [Route("Cadastro")]
    public IActionResult Cadastro(TransacaoModel model)
    {
      return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}