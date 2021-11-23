using AplicativoWebAcademiaTrainee.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace AplicativoWebAcademiaTrainee.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(ILogger<EmpresaController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.EmpresaModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind("Codigo,Nome,NomeFantasia,CNPJ")] EmpresaModel empresaModel)
        {
            return View("~/Views/Home/Index.cshtml");
        }

    }
}
