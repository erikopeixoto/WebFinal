using System;
using System.Security.Cryptography;
using System.Net;
using System.Web.Mvc;
using Modelos;
using Servicos;
using System.Web.Security;
using System.Text;
using System.IO;

namespace WEBTeste.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioServico serv = new UsuarioServico();
        private const string initVector = "r5dm5fgm24mfhfku";
        private const string passPhrase = "yourpassphrase";
        private const int keysize = 256;


        // GET: Usuario
        [Authorize]
        public ActionResult Index()
        {
            return View(serv.ObterUsuarios());
        }

        // GET: Usuario/Details/5
        [Authorize]
        public ActionResult Details(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = serv.ObterUsuario(Pid.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,nmUsuario,dsEmail,dsSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.dsSenha = Encrypt(usuario.dsSenha);
                serv.IncluirUsuario(usuario);
                if (!User.Identity.IsAuthenticated)
                {
                    return Login(usuario.dsEmail, usuario.dsSenha);
                }
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuario/Edit/5
        [Authorize]
        public ActionResult Edit(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = serv.ObterUsuario(Pid.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            usuario.dsSenha = Decrypt(usuario.dsSenha);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nmUsuario,dsEmail,dsSenha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.dsSenha = Encrypt(usuario.dsSenha);
                serv.AtualizarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        [Authorize]
        public ActionResult Delete(int? Pid)
        {
            if (Pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = serv.ObterUsuario(Pid.Value);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int Pid)
        {
            Usuario usuario = serv.ObterUsuario(Pid);
            serv.ExcluirUsuario(usuario);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.ReturnUrl = Request.QueryString["ReturnUrl"];
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string Pemail, string Psenha)
        {
            try
            {
                Usuario usuario = serv.RealizarLogin(Pemail, Encrypt(Psenha));

                if (usuario != null)
                {
                    FormsAuthentication.SetAuthCookie(usuario.nmUsuario, false);
                    string returnUrl = Request.QueryString["ReturnUrl"];
                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                
                ViewBag.Message = "Email e/ou senha inválidos";

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public static string Encrypt(string senha)
        {
            if (senha == "" || senha == null)
            {
                return "";
            }

            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(senha);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string senha)
        {
            if (senha == "" || senha == null)
            {
                return "";
            }

            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(senha);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
