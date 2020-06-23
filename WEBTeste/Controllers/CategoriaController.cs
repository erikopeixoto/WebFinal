using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Servicos;
using Modelos;

namespace WEBTeste.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        CategoriaServico serv = new CategoriaServico();
        ProdutoServico repProd = new ProdutoServico();

        // GET: Categoria
        public ActionResult Index(int? pagina)
        {
            int linhas = 5;
            int qtCat = serv.ObterQuantidadeCategoria();
            if (qtCat <= linhas)
                ViewBag.Pages = 1;
            else
                ViewBag.Pages = Convert.ToInt32(qtCat / linhas) + 1;

            ViewBag.Page = pagina.GetValueOrDefault(1);
            return View(serv.ObterCategorias(pagina.GetValueOrDefault(1), linhas));
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = serv.ObterCategoria(Pid.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Produtos/5
        public ActionResult Produto(int? Pid , string Pnome)
        {
            if (Pid == null ||
                Pnome == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Produto> produto = repProd.ObterProdCategoria(Pid.Value);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.NomeCategoria = Pnome;
            return View(produto);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NmCategoria")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                serv.IncluirCategoria(categoria);
                return RedirectToAction("Index");
            }

            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = serv.ObterCategoria(Pid.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NmCategoria")] Categoria Pcategoria)
        {
            if (ModelState.IsValid)
            {
                serv.AtualizarCategoria(Pcategoria);
                return RedirectToAction("Index");
            }
            return View(Pcategoria);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = serv.ObterCategoria(Pid.Value);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Pid)
        {
            Categoria categoria = serv.ObterCategoria(Pid);
            serv.ExcluirCategoria(categoria);
            return RedirectToAction("Index");
        }
    }
}
