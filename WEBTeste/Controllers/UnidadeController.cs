using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelos;
using Servicos;

namespace WEBTeste.Controllers
{
    public class UnidadeController : Controller
    {
        UnidadeServico serv = new UnidadeServico();

        // GET: Unidade
        public ActionResult Index(int? pagina)
        {
            int linhas = 5;
            int qtCat = serv.ObterQuantidadeUnidade();
            if (qtCat <= linhas)
                ViewBag.Pages = 1;
            else
                ViewBag.Pages = Convert.ToInt32(qtCat / linhas) + 1;

            ViewBag.Page = pagina.GetValueOrDefault(1);
            return View(serv.ObterUnidades(pagina.GetValueOrDefault(1), linhas));
        }

        // GET: Unidade/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade unidade = serv.ObterUnidade(id);
            if (unidade == null)
            {
                return HttpNotFound();
            }
            return View(unidade);
        }

        // GET: Unidade/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SgUnidade,DsUnidade")] Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                serv.IncluirUnidade(unidade);
                return RedirectToAction("Index");
            }

            return View(unidade);
        }

        // GET: Unidade/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade unidade = serv.ObterUnidade(id);
            if (unidade == null)
            {
                return HttpNotFound();
            }
            return View(unidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SgUnidade,DsUnidade")] Unidade unidade)
        {
            if (ModelState.IsValid)
            {
                serv.AtualizarUnidade(unidade);
                return RedirectToAction("Index");
            }
            return View(unidade);
        }

        // GET: Unidade/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unidade unidade = serv.ObterUnidade(id);
            if (unidade == null)
            {
                return HttpNotFound();
            }
            return View(unidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Unidade unidade = serv.ObterUnidade(id);
            serv.ExcluirUnidade(unidade);
            return RedirectToAction("Index");
        }
    }
}
