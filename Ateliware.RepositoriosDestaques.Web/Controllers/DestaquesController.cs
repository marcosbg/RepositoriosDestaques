using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ateliware.RepositoriosDestaques.Application.Services;
using Ateliware.RepositoriosDestaques.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ateliware.RepositoriosDestaques.Web.Controllers
{
    public class DestaquesController : Controller
    {
        private readonly IDestaquesApplicationService _destaquesApplicationService;

        public DestaquesController(IDestaquesApplicationService destaquesApplicationService)
        {
            _destaquesApplicationService = destaquesApplicationService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(new List<RepositoriosListagemViewModel>());
        }

        [HttpPost]
        public IActionResult PesquisarDestaques()
        {
            var listaRepositorios = _destaquesApplicationService.ListarRepositoriosDestaques();
            return View("Index", listaRepositorios);
        }

        public IActionResult VisualizarDetalhes(int id)
        {
            return View();
        }
    }
}
