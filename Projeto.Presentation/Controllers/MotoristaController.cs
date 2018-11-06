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
using System.Text;
using Projeto.Presentation.Utils;

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
                        erros[state.Key] = state.Value.Errors.Select(e => e.ErrorMessage).First();
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
                //retornar erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
       }

        //método para retornar  1 Motorista pelo id..
        public JsonResult ObterMotorista(int idMotorista)
        {
            try
            {
                //buscar 1 motorista no banco de dados pelo id..
                MotoristaRepository rep = new MotoristaRepository();
                Motorista m = rep.FindById(idMotorista);

                //retornando para a página..
                MotoristaConsultaViewModel model = new MotoristaConsultaViewModel();
                model.IdMotorista = m.IdMotorista;
                model.Nome = m.Nome;

                //enviando para a página..
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar mensagem de erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //metodo para excluir um motorista..
        public JsonResult ExcluirMotorista(int idMotorista)
        {
            try
            {
                //buscar o motorista na base de dados pelo id..
                MotoristaRepository rep = new MotoristaRepository();
                int qtdVeiculos = rep.QtdVeiculos(idMotorista);

                if (qtdVeiculos > 0)
                {
                    return Json($"O Motorista não pode ser excluido, pois possui {qtdVeiculos} veículos cadastrados.",
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Motorista m = rep.FindById(idMotorista);

                    //excluindo o motorista..
                    rep.Delete(m);

                    //retornando mensagem de sucesso..
                    return Json($"Motorista {m.Nome}, excluído com sucesso.",
                    JsonRequestBehavior.AllowGet);

                }
                
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //método para atualizar motorista..
        public JsonResult AtualizarMotorista(MotoristaEdicaoViewModel model)
        {
            try
            {
                //criando um objeto da classe de entidade..
                Motorista m = new Motorista();
                m.IdMotorista = model.IdMotorista;
                m.Nome = model.Nome;

                MotoristaRepository rep = new MotoristaRepository();
                rep.Update(m); //atualizando..

                return Json($"Motorista {m.Nome}, atualizado com sucesso.");
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }


        //método para retornar o relatorio de Motoristas..
        public void Relatorio()
        {
            //criando o conteudo do relatorio..
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1 class='titulo'>Relatório de Motoristas</h1>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now} </p>");
            conteudo.Append("<br/>");

            conteudo.Append("<table>");
            conteudo.Append("<tr>");
            conteudo.Append("<th>Código do Motorista</th>");
            conteudo.Append("<th>Nome do Motorista</th>");
            conteudo.Append("</tr>");

            MotoristaRepository rep = new MotoristaRepository();

            foreach (Motorista m in rep.FindAll())
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td>{m.IdMotorista}</td>");
                conteudo.Append($"<td>{m.Nome}</td>");
                conteudo.Append("</tr>");
            }

            conteudo.Append("</table>");

            //buscando o arquivo CSS..
            var css = Server.MapPath("/css/relatorio.css");

            //transformando o conteudo em arquivo PDF..
            ReportsUtil util = new ReportsUtil();
            byte[] pdf = util.GetPDF(conteudo.ToString(), css);

            //Download..
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=relatorio.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Response.BinaryWrite(pdf);
            Response.End();
        }



    }
}