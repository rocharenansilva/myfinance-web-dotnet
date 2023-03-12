using Microsoft.AspNetCore.Mvc;
using myfinance_web_dotnet.Domain.Services.Interfaces;
using myfinance_web_dotnet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_dotnet.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
    private readonly ILogger<TransacaoController> _logger;
    private readonly ITransacaoService _transacaoService;
    private readonly IPlanoContaService _planoContaService;

    public TransacaoController(
      ILogger<TransacaoController> logger,
      ITransacaoService transacaoService,
      IPlanoContaService planoContaService
    )
    {
      _logger = logger;
      _transacaoService = transacaoService;
      _planoContaService = planoContaService;
    }

    [HttpGet]
    [Route("Index")]
    public IActionResult Index()
    {
      ViewBag.ListaTransacoes = _transacaoService.ListarRegistros();

      return View();
    }

    [HttpGet]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(int? id)
    {
      var model = new TransacaoModel();

      if (id != null) {
        model = _transacaoService.RetornarRegistro((int)id);
      }

      var lista = _planoContaService.ListarRegistros();
      model.PlanoContas = new SelectList(lista, "Id", "Descricao");

      return View(model);
    }

    [HttpPost]
    [Route("Cadastro")]
    [Route("Cadastro/{id}")]
    public IActionResult Cadastro(TransacaoModel model)
    {
      _transacaoService.Salvar(model);
      return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }

    [HttpGet]
    [Route("Excluir/{id}")]
    public IActionResult Excluir(int id)
    {
      _transacaoService.Excluir((int)id);
      return RedirectToAction("Index");
    }
  }
}