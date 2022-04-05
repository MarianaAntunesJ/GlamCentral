using GlamCentral.Libraries.Filter;
using GlamCentral.Models;
using GlamCentral.Models.Enums;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    //[FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class AgendaController : Controller
    {
        private IAgendaRepository _repository;

        public AgendaController(IAgendaRepository respository)
        {
            _repository = respository;

        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Buscar(Agenda agenda)
        {
            var events = _repository.ObterTodosAgendamentos().ToList();
            return new JsonResult(events);
        }

        /*var v = new
        {
            subject = "teste",
            start = "2022-04-04T03:00:00+00:00",
            end = "2022-04-04T04:00:00+00:00",
            description = "Progressiva"
        };

        // Jeito de certo de salvar o objeto
        var events = new Teste
        {
            EventID = 1,
            Subject = "teste teste teste teste testetesteteste",
            Start = DateTime.Parse("2022-04-04 03:00"),
            End = DateTime.Parse("2022-04-04 04:00"),
            Description = "Progressiva"
        };

        var s = "[{ 'title':'All Day Event','start':'2022-04-01'},{ 'title':'Long Event','start':'2022-04-07','end':'2022-04-10'},{ 'groupId':'999','title':'Repeating Event','start':'2022-04-09T16:00:00+00:00'},{ 'groupId':'999','title':'Repeating Event','start':'2022-04-16T16:00:00+00:00'},{ 'title':'Conference','start':'2022-04-03','end':'2022-04-05'},{ 'title':'Meeting','start':'2022-04-04T10:30:00+00:00','end':'2022-04-04T12:30:00+00:00'},{ 'title':'Lunch','start':'2022-04-04T12:00:00+00:00'},{ 'title':'Birthday Party','start':'2022-04-05T07:00:00+00:00'},{ 'url':'http','title':'Click for Google','start':'2022-04-28'}]";

        var lista = new List<Teste>() { events };

        //return new JsonResult(_agendaRepository.ObterTodosAgendamentos());
        return new JsonResult(lista);
                }*/

        [HttpPost]
        public JsonResult Cadastrar(Agenda agenda)
        {
            var status = false;
            if (_repository.Cadastrar(agenda))
                status = true;

            return new JsonResult(status);
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var status = false;
            if (_repository.Excluir(id))
                status = true;

            return new JsonResult(status);
        }
    }

    public class Teste
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }
        public string A { get; set; }
    }
}
