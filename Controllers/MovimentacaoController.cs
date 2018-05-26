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
    public class MovimentacaoController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Movimentacao
        public ActionResult Index()
        {
            return View(db.Movimentacoes.ToList());
        }

        // GET: Movimentacao/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacoes.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movimentacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*Inclusão de veículos na movimentação*/
        public ActionResult Create([Bind(Include = "MovimentacaoID,Placa,Duracao,TempoCobrado,ValorTotal,Entrada,Saida")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacao.Entrada = DateTime.Now; 
                db.Movimentacoes.Add(movimentacao); 
                db.SaveChanges(); 
                return RedirectToAction("Index"); 
            }

            return View(movimentacao);
        }

        // GET: Movimentacao/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacoes.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovimentacaoID,Placa,Duracao,TempoCobrado,ValorTotal,Entrada,Saida")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimentacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movimentacao);
        }

        // GET: Movimentacao/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimentacao movimentacao = db.Movimentacoes.Find(id);
            if (movimentacao == null)
            {
                return HttpNotFound();
            }
            return View(movimentacao);
        }

        // POST: Movimentacao/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movimentacao movimentacao = db.Movimentacoes.Find(id);
            db.Movimentacoes.Remove(movimentacao);
            db.SaveChanges(); 
            return RedirectToAction("Index");
        }

        // GET: Movimentacao/Exit
        public ActionResult Exit(int id)
        {
            Movimentacao movimentacao = db.Movimentacoes.Find(id);

            
            var preco = db.Precos.FirstOrDefault(i => movimentacao.Entrada <= i.VigenciaFinal && movimentacao.Entrada >= i.VigenciaInicial);//Recupera o preço(com base na tabela de preços) praticado no dia da entrada 

            if (preco != null)
            {
                var tempoEstacionado = DateTime.Now.Subtract(movimentacao.Entrada).TotalMinutes;
                movimentacao.ValorTotal = 0;

                if (tempoEstacionado <= 10) 
                {
                    movimentacao.ValorTotal = 0; 

                }
                else
                    
                if (tempoEstacionado <= 30) 
                {
                    movimentacao.ValorTotal = preco.ValorHora / 2; 

                }
                else
                {
                    movimentacao.ValorTotal = movimentacao.ValorTotal + preco.ValorHora; 
                    tempoEstacionado -= 60; 

                    while (tempoEstacionado > 0) 
                    {
                        if (tempoEstacionado >= 10)
                        {
                            movimentacao.ValorTotal += preco.ValorHoraAdicional; 
                        }
                        tempoEstacionado -= 60;
                    }
                }
            }

            else
            {
                throw new Exception("Tabela de preços não cadastrada ou expirada");
            }
            
            movimentacao.Saida = DateTime.Now;
            
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
