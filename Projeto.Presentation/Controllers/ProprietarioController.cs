using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Presentation.Models;
using Projeto.Entities;
using Projeto.Repository.Persistence;
using System.Collections;
using System.Net;

namespace Projeto.Presentation.Controllers
{
    public class ProprietarioController : Controller
    {
        // GET: Proprietario/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Proprietario/Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        //JsonResult -> Receber chamadas AJAX (javascript)
        public JsonResult CadastrarProprietario(ProprietarioCadastroViewModel model)
        {
            //verificar  se os dados da model passaram nas validações..
            if (ModelState.IsValid)
            {
                try
                {
                    //entidade
                    Proprietario p = new Proprietario();
                    p.Nome = model.Nome;


                    //gravar no banco..
                    ProprietarioRepository rep = new ProprietarioRepository();
                    rep.Insert(p);

                    return Json($"Proprietario {p.Nome}, cadastrado com sucesso.");

                }
                catch (Exception e)
                {
                    //retornar mensagem de erro..
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


        //método para retornar a consulta de Proprietario para o Angular..
        public JsonResult ConsultarProprietario()
        {
            try
            {
                //declarar uma lista da classe ProprietarioConsultaViewModel..
                List<ProprietarioConsultaViewModel> lista = new List<ProprietarioConsultaViewModel>();

                //varrer cada proprietario obtido do banco de dados
                ProprietarioRepository rep = new ProprietarioRepository();
                foreach (Proprietario p in rep.FindAll())
                {
                    ProprietarioConsultaViewModel model = new ProprietarioConsultaViewModel();
                    model.IdProprietario= p.IdProprietario;
                    model.Nome = p.Nome;

                    lista.Add(model); //adicionando na lista..
                }

                //retornando a lista
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}