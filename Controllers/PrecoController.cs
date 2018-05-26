using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Park.Victor.Grein.Benner.DataAccessLayer;
using Park.Victor.Grein.Benner.Models;

namespace Benner.Victor.Grein.Estacionamento.Controllers
{
    public class PrecoController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Preco
        [HttpGet]
        public ActionResult Index()
        {
            var precos = db.Precos;
            return View(precos.ToList());
        }

        [HttpPost]
        // GET: Preco/Details/
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preco preco = db.Precos.Find(id);
            if (preco == null)
            {
                return HttpNotFound();
            }
            return View(preco);
        }

        // GET: Preco/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Preco/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Preco viewModel)
        {
            if (ModelState.IsValid)
            {
                Preco preco = new Preco
                {
                    ValorHora = viewModel.ValorHora, 
                    ValorHoraAdicional = viewModel.ValorHoraAdicional,
                    VigenciaFinal = viewModel.VigenciaFinal,
                    VigenciaInicial = viewModel.VigenciaInicial
                };

                db.Precos.Add(preco); 
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Preco/Edit/
        public ActionResult Edit(int id)
        {
            Preco preco = db.Precos.Find(id);
            if (preco == null)
            {
                return HttpNotFound();
            }
            return View(preco);
        }

        // POST: Preco/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Preco preco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(preco);
        }

        // GET: Preco/Delete/
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preco preco = db.Precos.Find(id);
            if (preco == null)
            {
                return HttpNotFound();
            }
            return View(preco);
        }

        // POST: Preco/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preco preco = db.Precos.Find(id);
            db.Precos.Remove(preco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
