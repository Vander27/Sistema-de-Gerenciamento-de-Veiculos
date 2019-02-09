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
        // GET: Usuario
  
        public ActionResult Autenticar()
        {
            return View();
        }

        // GET: Usuario/CriarConta
      
        public ActionResult CriarConta()
        {
            return View();
        }

        
              
                public ActionResult CadastrarUsuario(UsuarioCriarContaViewModel model)
                {
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            UsuarioRepository rep = new UsuarioRepository();

                            if(rep.HasEmail(model.Email))
                            {
                                ModelState.AddModelError("Email", errorMessage: "Este e-mail já foi cadastrado, por favor tente outro.");
                            }
                            else
                            {
                                Usuario u = new Usuario();
                                u.Nome = model.Nome;
                                u.Email = model.Email
                            ;
                                u.Senha = Criptografia.EncriptarSenha(model.Senha);

                                rep.Insert(u); //gravando..

                                ViewBag.Mensagem = $"Usuário {u.Nome}, cadastrado com sucesso.";
                                ModelState.Clear(); //limpar os campos do formulário
                            }

                        }
                        catch (Exception e)
                        {
                            //mensagem de erro..
                            ViewBag.Mensagem = e.Message;
                        }
                    }

                    //retornando para a página..
                    return View("CriarConta"); //nome da página..
                }
                

      
        public ActionResult AutenticarUsuario(UsuarioAutenticarViewModel model)
        {
            //verificando se a model não contem erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados pelo email e senha
                    UsuarioRepository rep = new UsuarioRepository();


                    // entidade..
                    Usuario u = rep.Obter(model.Email, Criptografia.EncriptarSenha(model.Senha));

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