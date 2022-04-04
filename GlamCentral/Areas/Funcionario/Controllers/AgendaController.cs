using GlamCentral.Database;
using GlamCentral.Libraries.Filter;
using GlamCentral.Models.Enums;
using GlamCentral.Repository.Interfaces;
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class AgendaController : Controller
    {
        private IAgendaRepository _agendaRepository;

        public AgendaController(IAgendaRepository agendaRespository)
        {
            _agendaRepository = agendaRespository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            return new JsonResult(null);
        }

        [HttpPost]
        public JsonResult SaveEvent(Event e)
        {
            return new JsonResult(null);
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            return new JsonResult(null);
        }
    }
}
