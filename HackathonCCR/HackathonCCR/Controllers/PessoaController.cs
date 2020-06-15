using HackathonCCR.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HackathonCCR.Controllers
{
    public class PessoaController : Controller
    {

        hackathon_ccrEntities bd = new hackathon_ccrEntities();
        // GET: Pessoa
        public ActionResult Cadastro(int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor, tente novamente.";
            }

            return View();
        }

        [HttpPost]
        public ActionResult Cadastro(DataPessoa pessoa, string url)
        {
            try
            {
                if (bd.pessoa.Any(a => a.email == pessoa.email))
                {
                    return RedirectToAction("Falha", "Cadastro", new { message = 1, url });
                }
                else
                {
                    endereco enderecoBanco = new endereco
                    {
                        complemento = pessoa.complemento,
                        numero = pessoa.numero,
                        estado = pessoa.estado,
                        endereco1 = pessoa.endereco,
                        cidade = pessoa.cidade,
                        bairro = pessoa.bairro
                    };


                    bd.endereco.Add(enderecoBanco);
                    bd.SaveChanges();

                    pessoa pessoaBanco = new pessoa
                    {
                        nome = pessoa.nome.ToUpper(),
                        email = pessoa.email.ToLower(),
                        dataNascimento = pessoa.dataNascimento,
                        senha = pessoa.senha,
                        contato = pessoa.contato,
                        enderecoId = enderecoBanco.enderecoId,
                        pontos = 25
                    };

                    bd.pessoa.Add(pessoaBanco);
                    bd.SaveChanges();

                    return RedirectToAction("CadastrarRodovia", "Pessoa", new { PessoaId = pessoaBanco.pessoaId });
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            //catch
            //{
            //    return RedirectToAction("Cadastro", "Pessoa", new { status = 1 });
            //}
        }

        //public void Message()
        //{
        //    const string accountSid = "AC82f8942b5a8a42dc073156d04684308e";
        //    const string authToken = "655130cb14dfb0a76a5c1c63a31cd819";

        //    TwilioClient.Init(accountSid, authToken);

        //    var messageOptions = new CreateMessageOptions(new PhoneNumber("whatsapp:+5575997145480"));
        //    messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
        //    messageOptions.Body = "Your Twilio code is 1238432";

        //    var message = MessageResource.Create(messageOptions);
        //    //var message = MessageResource.Create(
        //    //              to: new PhoneNumber("whatsapp:+5575988635142"),
        //    //              from: new PhoneNumber("whatsapp:+14155238886"), //559492831926
        //    //              body: "Hey from Twilio!"
        //    //           );
        //    Console.WriteLine("Message SID: " + message.Sid);
        //}
        public ActionResult CadastrarRodovia(int PessoaId, int? status)
        {
            if (status == 1)
            {
                ViewBag.message = "Ocorreu um erro ao processar a solicitação! Por favor, tente novamente.";
            }


            //int pessoaId = Convert.ToInt32(HttpContext.User.Identity.Name);

            Session["PessoaId"] = PessoaId;
            ViewBag.rodovias = bd.rodovia.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CadastrarRodovia(string[] mybox)
        {
            try
            {
                int pessoaId = Convert.ToInt32(Session["PessoaId"]);

                var rodovias = bd.rodovia.Where(x => x.nome != null && mybox.Contains(x.nome)).ToList();

                pessoa_rodovia pessoaRodovia = new pessoa_rodovia();

                foreach (var item in rodovias)
                {
                    pessoaRodovia.rodoviaId = item.rodoviaId;
                    pessoaRodovia.pessoaId = pessoaId;

                    bd.pessoa_rodovia.Add(pessoaRodovia);
                    bd.SaveChanges();
                }

                return RedirectToAction("Login", "Login", new { status = 3});

            }
            catch
            {
                return RedirectToAction("CadastroRodovia", "Pessoa", new { status = 1 });
            }
        }

        public ActionResult Falha(int status, string url)
        {
            if (status == 1)
            {
                ViewBag.message = "Usuário já cadastrado!";
                ViewBag.botao = "Fazer Login";
                ViewBag.esqueci = true;
                ViewBag.url = "http://www.adventista.edu.br/";
            }
            else if (status == 2)
            {

                ViewBag.message = "CPF não localizado!";
                ViewBag.botao = "Cadastre-se!";
                ViewBag.esqueci = false;
                ViewBag.url = "http://www2.adventista.edu.br/captiveportalad/";
            }
            ViewBag.url_retorno = url;
            return View();
        }

        public ActionResult Sucesso(int status, string url)
        {
            if (status == 1)
            {
                ViewBag.message = "Cadastro realizado com Sucesso!";
                ViewBag.botao = 1;
            }
            else if (status == 2)
            {

                ViewBag.message = "Conta OK! Você já pode acessar a Internet.";
                ViewBag.botao = 2;
            }
            else if (status == 3)
            {

                ViewBag.message = "Sua senha foi alterada com sucesso!";
                ViewBag.botao = 2;
            }
            else if (status == 4)
            {

                ViewBag.message = "Confira sua caixa postal! Enviamos uma mensagem de e-mail com orientações para alteração de sua senha.";
                ViewBag.botao = 3;
            }
            ViewBag.url_retorno = url;
            return View();
        }
    }
}