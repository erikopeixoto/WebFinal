using System;
using System.Collections.Generic;
using System.Net;
using Modelos;
using System.Web.Mvc;
using Servicos;

namespace WEBTeste.Controllers
{
    [Authorize]
    public class NotaFiscalController : Controller
    {
        NotaFiscalServico serv = new NotaFiscalServico();
        ClienteController cli = new ClienteController();
        ItensNotaFiscalServico itens = new ItensNotaFiscalServico();
        ProdutoController prod = new ProdutoController();
        MensagemServico servmsg = new MensagemServico();

        // GET: Cliente
        public ActionResult Index()
        {
            var NotaFiscal = serv.InstanciarNotaFiscal();

            NotaFiscal.DtMovimento = DateTime.Now;
            ViewData.Model = NotaFiscal;
            ViewBag.mdNota = CriaList();

            return View();
        }

        // GET: Cliente/Details/5
        public ActionResult Detalhe(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var notaFiscal = serv.InstanciarNotaFiscal();
            notaFiscal = serv.ObterNotaFiscal(Pid.Value);
            if (notaFiscal == null)
            {
                return HttpNotFound();
            }
            return View(notaFiscal);
        }

        [HttpPost]
        public JsonResult SalvarNF(NotaFiscal notaFiscal)
        {
            var msg = servmsg.InstanciarMensagem();

            try
            {
                if (ModelState.IsValid)
                {
                    if (notaFiscal.ID == 0)
                        msg = serv.IncluirNotaFiscal(notaFiscal);
                    else
                        msg = serv.AtualizarNotaFiscal(notaFiscal);
                }
            }
            catch (Exception e)
            {
                msg.Codigo = 0;
                msg.Descricao = e.Message.ToString();
            }
            return Json(msg);
        }

        [HttpPost]
        public JsonResult ExcluirNF(int pIDNF)
        {
            var msg = servmsg.InstanciarMensagem();
            try
            {
                var NotaFiscal = serv.ObterNotaFiscal(pIDNF);
                msg = serv.ExcluirNotaFiscal(NotaFiscal);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = e.Message.ToString();
            }
            return Json(msg);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var NotaFiscal = serv.InstanciarNotaFiscal();
            NotaFiscal = serv.ObterNotaFiscal(Pid.Value);
            if (NotaFiscal == null)
            {
                return HttpNotFound();
            }
            ViewBag.TpCliente = new SelectList(CriaList(), "Value", "Text");
            ViewData["tipoCliente"] = ViewBag.TpCliente;
            return View(NotaFiscal);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NmCliente,DsEndereco,TpCliente,DtCriacao")] NotaFiscal notaFiscal,
                                     FormCollection pForm)
        {
            if (ModelState.IsValid)
            {
                serv.AtualizarNotaFiscal(notaFiscal);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }

        private IEnumerable<SelectListItem> CriaList()
        {
            IEnumerable<SelectListItem> mdNota = new[]{
                            new SelectListItem() { Text = "NF Eletrônica", Value = "55", Selected = true },
                            new SelectListItem() { Text = "Modelo 1", Value = "1" }};
            return mdNota;
        }

        public ActionResult Clientes(string pNmCliente, long? pNuCnpjCpf)
        {
            pNmCliente = pNmCliente.Replace("%", " ");
            if (pNuCnpjCpf == null)
                pNuCnpjCpf = 0;

            return PartialView("_Clientes", cli.ObterClientesNome(pNmCliente, pNuCnpjCpf.Value));

        }

        public PartialViewResult Produtos(string pNmProduto)
        {
            pNmProduto = pNmProduto.Replace("%", " ");
            return PartialView("_Produtos", prod.ObterProdutosNome(pNmProduto));

        }

        public PartialViewResult ListarItensNF(long pNFID)
        {
            return PartialView("_ListaItensNF", serv.ObterItensNF(pNFID));
        }

        public ActionResult IncluirItens()
        {
            return PartialView("_TelaItens");
        }

        public ActionResult EditarItens(long? pID)
        {
            var ItemNF = itens.InstanciarItensNF();
            if (pID != null)
                ItemNF = itens.ObterItemNF(pID.Value);

            return PartialView("_TelaItens", ItemNF);

        }

        public JsonResult LerNotaFiscal(long NuNota, int SeNota, int MdNota)
        {
            var NF = serv.ObterNotaFiscal(NuNota, SeNota, MdNota);
            if (NF != null)
            {
                var retorno = new 
                {
                    NuNota = NF.NuNota,
                    SeNota = NF.SeNota,
                    MdNota = NF.MdNota,
                    ClienteId = NF.ClienteId,
                    NmCliente = NF.Cliente.NmCliente.Trim(),
                    CPFCNPJFormatado = NF.Cliente.CPFCNPJFormatado,
                    NFID = NF.ID,
                    NuChaveAcesso = NF.NuChaveAcesso,
                    VlNota = NF.VlNota.ToString().Replace('.',',')
                };
                 return Json(retorno, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObterProduto(int pID)
        {
            var produto = prod.ObterProduto(pID);
            var retorno = new
                {
                    NomeProduto = (produto != null ? produto.nmProduto : ""),
                    Preco = (produto != null ? produto.vlPreco : 0),
                    Unidade = (produto != null ? produto.Unidade.DsUnidade : ""),
                    SgUnidade = (produto != null ? produto.SgUnidade : "")
                };

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public long LerNumeroNF(int SeNota, int MdNota)
        {

            return serv.ObterNumeroNF(SeNota, MdNota);
        }

        [HttpPost]
        public JsonResult SalvarItem(ItensNotaFiscal item)
        {
            var msg = servmsg.InstanciarMensagem();

            try
            {
                if (item.ID == 0)
                    msg = itens.IncluirItensNF(item);
                else
                {
                    msg = itens.AtualizarItensNF(item);
                }
            }
            catch (Exception e)
            {
                msg.Codigo = 0;
                msg.Descricao = e.Message.ToString();
            }
            return Json(msg);
        }

        [HttpPost]
        public JsonResult ExcluirItem(long pitem)
        {
            var msg = servmsg.InstanciarMensagem();
            var itemNF = itens.ObterItemNF(pitem);
            try
            {

                msg = itens.ExcluirItensNF(itemNF);
            }
            catch (Exception e)
            {
                msg.Codigo = 999;
                msg.Descricao = e.Message.ToString();
            }
            return Json(msg);
        }

    }
}
