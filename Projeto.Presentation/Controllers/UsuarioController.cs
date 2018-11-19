using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Presentation.Models;
using Projeto.Entities;
using Projeto.Repository.Persistence;
using System.Web.Security;
using Projeto.Util;
using System.Collections;
using System.Net;

namespace Projeto.Presentation.Controllers
{
    public class UsuarioController : Controller
    {


        // GET: Usuario/Cadastrar/Autenticar
        public ActionResult Autenticar()
        {
            return View();
        }

        // GET: Usuario/Cadastrar/Autenticar
        public ActionResult CriarConta()
        {
            return View();
        }

        






        [HttpPost] //recebe requisições do tipo POST (FormMethod.Post)
        public JsonResult CadastrarUsuario(UsuarioCriarContaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //entidade..
                    Usuario u = new Usuario();
                    u.Nome = model.Nome;
                    u.Email = model.Email;
                    u.Senha = Criptografia.EncriptarSenha(model.Senha);

                    //gravar no banco..
                    UsuarioRepository rep = new UsuarioRepository();
                    rep.Insert(u); //gravando..

                    ModelState.Clear(); //limpar os campos do formulário

                    return Json($"Usuario {u.Nome}, cadastrado com sucesso.");


                }
                catch (Exception e)
                {
                    //mensagem de erro..
                    return Json(e.Message);
                }

            }

            else
            {
                //criar uma rotina para retornar as mensagens de erro de 
                //validação para cada campo da classe viewModel..
                Hashtable erros = new Hashtable();

                //varrer o objeto ModelState..
                foreach (var state in ModelState)
                {
                    //verificar se o elemento contem erro..
                    if (state.Value.Errors.Count > 0)
                    {
                        //adicionar o erro dentro do Hastable
                        erros[state.Key] = state.Value.Errors
                            .Select(e => e.ErrorMessage).First();
                    }
                }

                //retornar erros de validaçâo..STATUS 400
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(erros);

            }

        }




        [HttpPost]
        public ActionResult AutenticarUsuario(UsuarioAutenticarViewModel model)
        {
            //verificando se a model não contem erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados pelo email e senha
                    UsuarioRepository rep = new UsuarioRepository();

                    //entidade..
                    Usuario u = rep.Find(model.Email, Criptografia.EncriptarSenha(model.Senha));
                  

                    //verifica se o usuario não é null
                    if (u != null)
                    {
                        FormsAuthenticationTicket ticket =
                            new FormsAuthenticationTicket(u.Email, false, 10);

                        //gravar o ticket em cookie
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));

                        Response.Cookies.Add(cookie);


                        //redirecionar para a área restrita
                        return RedirectToAction("Index", "Principal",
                    new { area = "AreaRestrita" });
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso Negado. Usuário não encontrado.";
                    }
                }
                catch (Exception e)
                {
                    //exibir mensagem de erro..
                    ViewBag.Mensagem = "Ocorreu um erro: " + e.Message;
                }
            }

            return View("Autenticar"); //nome da página
        }

        public ActionResult Logout()
        {
            //destruir o ticket do usuario gravado
            //em cookie no navegador
            FormsAuthentication.SignOut();

            //redirecionar para a página de login
            return View("Autenticar");


        }
    }
}