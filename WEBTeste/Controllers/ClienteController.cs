using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Modelos;
using Servicos;
using WEBTeste.Util;

namespace WEBTeste.Controllers
{
    [Authorize]
    public class ClienteController : Controller
    {
        ClienteServico serv = new ClienteServico();
        TratarString tratar = new TratarString();
        IEnumerable<SelectListItem> tpCliente = new[]{
                            new SelectListItem() { Text = "Física", Value = "F" } ,
                            new SelectListItem() { Text = "Jurídica", Value = "J" }};

        // GET: Cliente
        public ActionResult Index(int? pagina)
        {
            int linhas = 5;
            int qtCli = serv.ObterQuantidadeClientes();
            if (qtCli <= linhas)
                ViewBag.Pages = 1;
            else
                ViewBag.Pages = Convert.ToInt32(qtCli / linhas) + 1;

            ViewBag.Page = pagina.GetValueOrDefault(1);
            return View(serv.ObterClientes(pagina.GetValueOrDefault(1), linhas));
        }

        // GET: Cliente/Details/5
        public ActionResult Detalhe(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cliente = serv.InstanciarCliente();
            cliente = serv.ObterCliente(Pid.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Incluir()
        {
            var cliente = serv.InstanciarCliente();
            CriaList("F");
            cliente.DtCriacao = DateTime.Now;
            ViewData.Model = cliente;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Incluir([Bind(Include = "NmCliente,DsEndereco,TpCliente,DtCriacao")] Cliente cliente,
                                     FormCollection pForm)
        {
            if (ModelState.IsValid)
            {
                cliente.NuCep = Convert.ToInt32(tratar.RetirarEspLetra(pForm.GetValue("NuCep").AttemptedValue));
                cliente.NuCnpjCpf = Convert.ToInt64(tratar.RetirarEspLetra(pForm.GetValue("NuCnpjCpf").AttemptedValue));
                cliente.NuTelefone = Convert.ToInt64(tratar.RetirarEspLetra(pForm.GetValue("NuTelefone").AttemptedValue));
                serv.IncluirCliente(cliente);
                CriaList("F");
                return RedirectToAction("Index");
            }
            CriaList(cliente.TpCliente);
            ViewBag.Message = "Erro na inclusão.";

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cliente = serv.InstanciarCliente();
            cliente = serv.ObterCliente(Pid.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            CriaList(cliente.TpCliente);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NmCliente,DsEndereco,TpCliente,DtCriacao")] Cliente cliente,
                                     FormCollection pForm)
        {
            if (ModelState.IsValid)
            {
                cliente.NuCep = Convert.ToInt32(tratar.RetirarEspLetra(pForm.GetValue("NuCep").AttemptedValue));
                cliente.NuCnpjCpf = Convert.ToInt64(tratar.RetirarEspLetra(pForm.GetValue("NuCnpjCpf").AttemptedValue));
                cliente.NuTelefone = Convert.ToInt64(tratar.RetirarEspLetra(pForm.GetValue("NuTelefone").AttemptedValue));
                serv.AtualizarCliente(cliente);
                CriaList("F");
                return RedirectToAction("Index");
            }
            CriaList(cliente.TpCliente);
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Excluir(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cliente = serv.InstanciarCliente();
            cliente = serv.ObterCliente(Pid.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public ActionResult ExcluirConfirmed(int Pid)
        {
            var cliente = serv.InstanciarCliente();
            cliente = serv.ObterCliente(Pid);
            serv.ExcluirCliente(cliente);
            return RedirectToAction("Index");
        }

        private void CriaList(string pTpCliente)
        {
            ViewBag.TpCliente = new SelectList(tpCliente, "Value", "Text", pTpCliente);
        }

        public List<Cliente> ObterClientesNome(string pNmCliente, long pNuCnpjCpf)
        {
            if (pNuCnpjCpf != 0)
            {
                return serv.ObterPNome(pNmCliente, pNuCnpjCpf);
            }

            if (!string.IsNullOrEmpty(pNmCliente))
            {
                return serv.ObterPNome(pNmCliente, pNuCnpjCpf);
            }
            return null;
        }

        public string ObterNome (int? Pid)
        {
            if (Pid == null)
            {
                return "";
            }

            return serv.ObterNomeCliente(Pid.Value);
        }
    }
}
