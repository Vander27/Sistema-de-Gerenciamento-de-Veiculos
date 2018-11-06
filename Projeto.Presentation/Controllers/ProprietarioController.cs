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
        public JsonResult ConsultarProprietarios()
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


        //método para retornar  1 Proprietario pelo id..
        public JsonResult ObterProprietario(int idProprietario)
        {
            try
            {
                //buscar 1 Proprietario no banco de dados pelo id..
                ProprietarioRepository rep = new ProprietarioRepository();
                Proprietario p = rep.FindById(idProprietario);

                //retornando para a página..
                ProprietarioConsultaViewModel model = new ProprietarioConsultaViewModel();
                model.IdProprietario = p.IdProprietario;
                model.Nome = p.Nome;

                //enviando para a página..
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar mensagem de erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //metodo para excluir um proprietario..
        public JsonResult ExcluirProprietario(int idProprietario)
        {
            try
            {
                //buscar o Proprietario na base de dados pelo id..
                ProprietarioRepository rep = new ProprietarioRepository();
                int qtdVeiculos = rep.QtdVeiculos(idProprietario);

                if (qtdVeiculos > 0)
                {
                    return Json($"O Proprietário não pode ser excluído, pois possui {qtdVeiculos} veículos cadastrados.",
                        JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Proprietario p = rep.FindById(idProprietario);

                    //excluindo o Proprietario..
                    rep.Delete(p);

                    //retornando mensagem de sucesso..
                    return Json($"Proprietario {p.Nome}, excluído com sucesso.",
                    JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //método para atualizar proprietario..
        public JsonResult AtualizarProprietario(ProprietarioEdicaoViewModel model)
        {
            try
            {
                //criando um objeto da classe de entidade..
                Proprietario p = new Proprietario();
                p.IdProprietario = model.IdProprietario;
                p.Nome = model.Nome;

                ProprietarioRepository rep = new ProprietarioRepository();
                rep.Update(p); //atualizando..

                return Json($"Proprietario {p.Nome}, atualizado com sucesso.");
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }



        //método para retornar o relatorio de Proprietarios..
        public void Relatorio()
        {
            //criando o conteudo do relatorio..
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1 class='titulo'>Relatório de Proprietários</h1>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now} </p>");
            conteudo.Append("<br/>");

            conteudo.Append("<table>");
            conteudo.Append("<tr>");
            conteudo.Append("<th>Código do Proprietário</th>");
            conteudo.Append("<th>Nome do Proprietário</th>");
            conteudo.Append("</tr>");

            ProprietarioRepository rep = new ProprietarioRepository();

            foreach (Proprietario p in rep.FindAll())
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td>{p.IdProprietario}</td>");
                conteudo.Append($"<td>{p.Nome}</td>");
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