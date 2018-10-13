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
    public class MotoristaController : Controller
    {
        // GET: Motorista/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Motorista/Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        //JsonResult -> Receber chamadas Ajax (JavaScript)
        public JsonResult CadastrarMotorista(MotoristaCadastroViewModel model)
        {
            // verificar se os dados model passaram nas validacoes..
            if (ModelState.IsValid)
            {
                try
                {
                    // entidade..
                    Motorista m = new Motorista();
                    m.Nome = model.Nome;

                    //gravar no banco..
                    MotoristaRepository rep = new MotoristaRepository();
                    rep.Insert(m);

                    return Json($"Motorista {m.Nome}, cadastrado com sucesso.");
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
                //validacao para cada campo da classe viewmodel..
                Hashtable erros = new Hashtable();

                //varrer o objeto ModelState..
                foreach(var state in ModelState)
                {
                    //verificar se o elemento contem erro..
                    if (state.Value.Errors.Count > 0)
                    {
                        //adicionar o erro dentro do Hashtable..
                        erros[state.Key] = state.Value.Errors
                            .Select(e => e.ErrorMessage).First();
                    }
                }


                // retornar erros de Validação.. Status 400
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(erros);
            }
        }

       //método para retornar a consulta de Motoristas para o Angular..
       public JsonResult ConsultarMotoristas()
        {
            try
            {
                //declarar uma lista da classe MotoristaConsultaViewModel..
                List<MotoristaConsultaViewModel> lista = new List<MotoristaConsultaViewModel>();

                //varrer cada motorista abtido do banco de dados
                MotoristaRepository rep = new MotoristaRepository();
                foreach(Motorista m in rep.FindAll())
                {
                    MotoristaConsultaViewModel model = new MotoristaConsultaViewModel();
                    model.IdMotorista = m.IdMotorista;
                    model.Nome = m.Nome;

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