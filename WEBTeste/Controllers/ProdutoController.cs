using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Modelos;
using Servicos;

namespace WEBTeste.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        ProdutoServico serv = new ProdutoServico();
        UsuarioServico servUsu = new UsuarioServico();
        CategoriaServico servCat = new CategoriaServico();
        UnidadeServico servUni = new UnidadeServico();

        // GET: Produto
        public ActionResult Index(int? pagina)
        {
            int linhas = 5;
            int qtProd = serv.ObterQuantidadeProdutos();
            if (qtProd <= linhas)
                ViewBag.Pages = 1;
            else
                ViewBag.Pages = Convert.ToInt32(qtProd / linhas) + 1;

            ViewBag.Page = pagina.GetValueOrDefault(1);
            return View(serv.ObterProdutos(pagina.GetValueOrDefault(1), linhas));
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = serv.ObterProduto(Pid.Value);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            var produto = serv.InstanciarProduto();
            var usuario = servUsu.ObterUsuario(User.Identity.Name);
            
            CriarViewBag(produto);
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nmProduto,dsProduto,vlPreco,qtEstoque,IdUserCreate,CategoriaId")] Produto produto, FormCollection Pform)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = servUsu.ObterUsuario(User.Identity.Name);
                produto.IdUserCreate = usuario.ID;
                produto.SgUnidade = Pform.GetValue("sgUnidade").AttemptedValue;
                serv.IncluirProduto(produto);
                return RedirectToAction("Index");
            }
            CriarViewBag(produto);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var produto = serv.InstanciarProduto();
            produto = serv.ObterProduto(Pid.Value);
            if (produto == null)
            {
                return HttpNotFound();
            }
            CriarViewBag(produto);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nmProduto,dsProduto,vlPreco,qtEstoque,CategoriaId,IdUserCreate")] Produto produto, FormCollection Pform)
        {
            if (ModelState.IsValid)
            {
                //Usuario usuario = servositorio.ObterUsuario(User.Identity.Name);
                Usuario usuario = servUsu.ObterUsuario(Convert.ToInt16(Pform.GetValue("IdUserCreate").AttemptedValue));
                produto.IdUserCreate = usuario.ID;
                Categoria categoria = servCat.ObterCategoria(Convert.ToInt16(Pform.GetValue("CategoriaId").AttemptedValue));
                produto.CategoriaId = categoria.ID;
                produto.SgUnidade = Pform.GetValue("SgUnidade").AttemptedValue;
                serv.AtualizarProduto(produto);
                return RedirectToAction("Index");
            }
            CriarViewBag(produto);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = serv.ObterProduto(Pid.Value);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Pid)
        {
            Produto produto = serv.ObterProduto(Pid);
            serv.ExcluirProduto(produto);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public PartialViewResult List()
        {
            return PartialView("_Lista", serv.ObterProdutos());
        }

        private void CriarViewBag(Produto produto)
        {
            if (produto.ID == 0)
            {
                ViewBag.IdUserCreate = new SelectList(servUsu.ObterUsuarios(), "ID", "nmUsuario");
                ViewBag.CategoriaId = new SelectList(servCat.ObterCategoriasT(), "ID", "NmCategoria");
                ViewBag.SgUnidade = new SelectList(servUni.ObterUnidadesT(), "SgUnidade", "DsUnidade");
            }
            else
            {
                ViewBag.IdUserCreate = new SelectList(servUsu.ObterUsuarios(), "ID", "nmUsuario", produto.IdUserCreate);
                ViewBag.CategoriaId = new SelectList(servCat.ObterCategoriasT(), "ID", "NmCategoria", produto.CategoriaId);
                ViewBag.SgUnidade = new SelectList(servUni.ObterUnidadesT(), "SgUnidade", "DsUnidade", produto.SgUnidade);
            }
        }

        public List<Produto> ObterProdutosNome(string pnmProduto)
        {
            return serv.ObterPNome(pnmProduto);
        }

        public string ObterNome(int? Pid)
        {
            if (Pid == null)
            {
                return "";
            }

            return serv.ObterNomeProduto(Pid.Value);
        }

        public Produto ObterProduto(int? Pid)
        {
            var produto = serv.InstanciarProduto();

            if (Pid == null)
            {
                return produto;
            }
            produto = serv.ObterProduto(Pid.Value);

            return produto;
        }
    }
}
