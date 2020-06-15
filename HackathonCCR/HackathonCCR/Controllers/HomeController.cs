using HackathonCCR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HackathonCCR.Controllers
{
    public class HomeController : Controller
    {
        hackathon_ccrEntities bd = new hackathon_ccrEntities();
        // GET: Home
        public ActionResult Index(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor, tente novamente.";
            }

            ViewBag.sensacoes = bd.sente.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Index(string[] mybox)
        {
            try
            {
                var sentimentos = bd.sente.Where(x => x.nome != null && mybox.Contains(x.nome)).ToList();

                pessoa_sente pessoaSente = new pessoa_sente();

                int pessoaId = Convert.ToInt32(Session["PessoaId"]);

                foreach (var item in sentimentos)
                {
                    pessoaSente.senteId = item.senteId;
                    pessoaSente.pessoaId = pessoaId;
                    pessoaSente.data = DateTime.Now;

                    bd.pessoa_sente.Add(pessoaSente);
                    bd.SaveChanges();
                }

                return RedirectToAction("CheckUp", "Home");

            }
            catch
            {
                return RedirectToAction("Index", "Home", new { status = 1 });
            }
        }

        public ActionResult CheckUp(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor, tente novamente.";
            }

            return View();
        }

        public ActionResult Resultado(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor, tente novamente.";
            }

            return View();
        }
    }
}