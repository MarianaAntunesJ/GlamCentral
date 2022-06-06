using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    public class GraficoController : Controller
    {
        // GET: GraficoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GraficoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GraficoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GraficoController/Create
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GraficoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GraficoController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GraficoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GraficoController/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
