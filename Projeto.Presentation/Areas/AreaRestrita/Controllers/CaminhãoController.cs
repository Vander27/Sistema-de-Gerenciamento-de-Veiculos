
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
using Projeto.Presentation.Filters;


namespace Projeto.Presentation.Areas.AreaRestrita.Controllers
{

    public class CaminhaoController : Controller
    {
        // GET: Caminhao/Cadastro..
        [NoCache]
        [Authorize]
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Caminhao/Consulta..
        [NoCache]
        [Authorize]
        public ActionResult Consulta()
        {
            return View();
        }




        //JsonResult -> Receber chamadas AJAX (JavaScript)..

        public JsonResult CadastrarCaminhao(CaminhaoCadastroViewModel model)
        {
            //verificar se os dados da model passaram nas validações..
            if (ModelState.IsValid)
            {
                try
                {
                    Caminhao c = new Caminhao();

                    c.Marca = model.Marca;
                    c.Modelo = model.Modelo;

                    c.Placa = model.Placa;

                    c.KmInicial = model.KmInicial;

                    c.Foto = model.Foto;
                    c.IdMotorista = model.IdMotorista;
                    c.IdProprietario = model.IdProprietario;

                    //gravar no banco..
                    CaminhaoRepository rep = new CaminhaoRepository();
                    rep.Insert(c);
                    return Json($"Caminhão {c.Modelo}, cadastrado com sucesso.");

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
                //valicação para cada campo da classe viewModel..
                Hashtable erros = new Hashtable();

                //varrer o objeto ModelState..
                foreach (var state in ModelState)
                {
                    //verificar se o elemento contem erro..
                    if (state.Value.Errors.Count > 0)
                    {
                        //adicionar o erro dentro do Hashtable..
                        erros[state.Key] = state.Value.Errors
                            .Select(e => e.ErrorMessage).First();
                    }

                }

                //retornar erros de validações.. STATUS 400..
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(erros);
            }
        }


        //método para retornar a consulta de aumomovel para o Angular..

        public JsonResult ConsultarCaminhoes()
        {
            try
            {
                //declarar uma lista da classe automovelConsultaViewModel..
                List<CaminhaoConsultaViewModel> lista = new List<CaminhaoConsultaViewModel>();

                //varrer cada Caminhao obtido do banco de dados
                CaminhaoRepository rep = new CaminhaoRepository();
                foreach (Caminhao c in rep.FindAll())
                {
                    CaminhaoConsultaViewModel model = new CaminhaoConsultaViewModel();

                    model.IdCaminhao = c.IdCaminhao;

                    model.Marca = c.Marca;
                    model.Modelo = c.Modelo;

                    model.Placa = c.Placa;

                    model.KmInicial = c.KmInicial;

                    model.Foto = c.Foto;
                    model.IdMotorista = c.IdMotorista;
                    model.NomeMotorista = c.Motorista.Nome;
                    model.IdProprietario = c.IdProprietario;
                    model.NomeProprietario = c.Proprietario.Nome;

                    lista.Add(model); //adicionando na lista..
                }

                //retornando a lista..
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //método  para retornar 1 Caminhao pelo id..
        public JsonResult ObterCaminhao(int idCaminhao)
        {
            try
            {
                //buscar 1 automovel no banco de dados pelo id..
                CaminhaoRepository rep = new CaminhaoRepository();
                Caminhao c = rep.FindById(idCaminhao);

                //retornando para a página..
                CaminhaoConsultaViewModel model = new CaminhaoConsultaViewModel();
                model.IdCaminhao = c.IdCaminhao;

                model.Marca = c.Marca;
                model.Modelo = c.Modelo;

                model.Placa = c.Placa;

                model.KmInicial = c.KmInicial;

                model.Foto = c.Foto;
                model.IdMotorista = c.IdMotorista;
                model.IdProprietario = c.IdProprietario;

                //enviando para a página..
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar mensagem de erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //método para excluir um  Caminhao..
        public JsonResult ExcluirCaminhao(int idCaminhao)
        {
            try
            {
                //buscar o Caminhao na base de dados pelo id..
                CaminhaoRepository rep = new CaminhaoRepository();
                Caminhao c = rep.FindById(idCaminhao);

                //excluindo o Caminhao
                rep.Delete(c);

                //retornando mensagem de sucesso..
                return Json($"Caminhão {c.Modelo},excluído com sucesso.",
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //método para atualizar Caminhao..
        public JsonResult AtualizarCaminhao(CaminhaoEdicaoViewModel model)
        {
            try
            {
                //criando um objeto da classe de entidade..
                Caminhao c = new Caminhao();
                c.IdCaminhao = model.IdCaminhao;

                c.Marca = model.Marca;
                c.Modelo = model.Modelo;

                c.Placa = model.Placa;

                c.KmInicial = model.KmInicial;

                c.Foto = model.Foto;
                c.IdMotorista = model.IdMotorista;
                c.IdProprietario = model.IdProprietario;


                CaminhaoRepository rep = new CaminhaoRepository();
                rep.Update(c);

                return Json($"Caminhão {c.Modelo}, atualizado com sucesso.");
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }


        //método para retornar o relatorio de Caminhao..
        public void Relatorio()
        {
            //criando o conteudo do relatorio..
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1 class='titulo'>Relatório de Caminhões</h1>");
            conteudo.Append("<h4 class='titulo'>VJC TECHNOLOGY</h4>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now} </p>");
            conteudo.Append("<br/>");

            conteudo.Append("<table>");
            conteudo.Append("<tr>");
            conteudo.Append("<th>Foto</th>");
            conteudo.Append("<th>Modelo</th>");
            conteudo.Append("<th>Marca</th>");

            conteudo.Append("<th>Placa</th>");

            conteudo.Append("<th>Km</th>");

            conteudo.Append("<th>Motorista</th>");
            conteudo.Append("<th>Proprietário</th>");
            conteudo.Append("</tr>");

            CaminhaoRepository rep = new CaminhaoRepository();

            foreach (Caminhao c in rep.FindAll())
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td><img src='{c.Foto}' height='60'/></td>");
                conteudo.Append($"<td>{c.Marca}</td>");
                conteudo.Append($"<td>{c.Modelo}</td>");


                conteudo.Append($"<td>{c.Placa}</td>");

                conteudo.Append($"<td>{c.KmInicial}</td>");

                conteudo.Append($"<td>{c.Motorista.Nome}</td>");
                conteudo.Append($"<td>{c.Proprietario.Nome}</td>");
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