using HackathonCCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HackathonCCR.Controllers
{
    public class LoginController : Controller
    {
        hackathon_ccrEntities bd = new hackathon_ccrEntities();
        public ActionResult Login(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro na solicitação, tente novamente";
            }
            else if (status == 2)
            {
                ViewBag.message = "Login ou senha inválidos!";
            }
            else if (status == 3)
            {
                ViewBag.message2 = "Usuário cadastrado com sucesso!";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(DataLogin user)
        {
            try
            {
                if (bd.pessoa.Any(x => x.email == user.email && x.senha == user.senha))
                {
                    var usuario = bd.pessoa.FirstOrDefault(x => x.email == user.email && x.senha == user.senha);

                    FormsAuthentication.SetAuthCookie(usuario.pessoaId.ToString(), true);

                    Session["PessoaId"] = usuario.pessoaId;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Login", "Login", new { status = 2 });
                }
            }
            catch
            {
                return RedirectToAction("Login", "Login", new { status = 1 });
            }
        }
    }
}