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
    public class AutomovelController : Controller
    {
        // GET: Automovel/Cadastro..
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Automovel/Consulta..
        public ActionResult Consulta()
        {
            return View();
        }

        //JsonResult -> Receber chamadas AJAX (JavaScript)..
  
        public JsonResult CadastrarAutomovel(AutomovelCadastroViewModel model)
        {
            //verificar se os dados da model passaram nas validações..
            if (ModelState.IsValid)
            {
                try
                {
                    Automovel a = new Automovel();
                    
                    a.Marca = model.Marca;
                    a.Modelo = model.Modelo;
                    a.Placa = model.Placa;
                    a.KmInicial = model.KmInicial;
                    a.Foto = model.Foto;
                    a.IdMotorista = model.IdMotorista;
                    a.IdProprietario = model.IdProprietario;

                    //gravar no banco..
                    AutomovelRepository rep = new AutomovelRepository();
                    rep.Insert(a);
                    return Json($"Automóvel {a.Modelo}, cadastrado com sucesso.");

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
     
        public JsonResult ConsultarAutomoveis()
        {
            try
            {
                //declarar uma lista da classe automovelConsultaViewModel..
                List<AutomovelConsultaViewModel> lista = new List<AutomovelConsultaViewModel>();

                //varrer cada automovel obtido do banco de dados
                AutomovelRepository rep = new AutomovelRepository();
                foreach (Automovel a in rep.FindAll())
                {
                    AutomovelConsultaViewModel model = new AutomovelConsultaViewModel();
                    
                    model.IdAutomovel = a.IdAutomovel;
                    
                    model.Marca = a.Marca;
                    model.Modelo = a.Modelo;
                    
                    model.Placa = a.Placa;
                    
                    model.KmInicial = a.KmInicial;
                    
                    model.Foto = a.Foto;
                    model.IdMotorista = a.IdMotorista;
                    model.NomeMotorista = a.Motorista.Nome;
                    model.IdProprietario = a.IdProprietario;
                    model.NomeProprietario = a.Proprietario.Nome;

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


        //método  para retornar 1 automovel pelo id..
        public JsonResult ObterAutomovel(int idAutomovel)
        {
            try
            {
                //buscar 1 automovel no banco de dados pelo id..
                AutomovelRepository rep = new AutomovelRepository();
                Automovel a = rep.FindById(idAutomovel);

                //retornando para a página..
                AutomovelConsultaViewModel model = new AutomovelConsultaViewModel();
                model.IdAutomovel = a.IdAutomovel;
                
                model.Marca = a.Marca;
                model.Modelo = a.Modelo;
                
                model.Placa = a.Placa;
               
                model.KmInicial = a.KmInicial;
                
                model.Foto = a.Foto;
                model.IdMotorista = a.IdMotorista;
                model.IdProprietario = a.IdProprietario;

                //enviando para a página..
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar mensagem de erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //método para excluir um  Automovel..
        public JsonResult ExcluirAutomovel(int idAutomovel)
        {
            try
            {
                //buscar o Automovel na base de dados pelo id..
                AutomovelRepository rep = new AutomovelRepository();
                Automovel a = rep.FindById(idAutomovel);

                //excluindo o Automovel
                rep.Delete(a);

                //retornando mensagem de sucesso..
                return Json($"Automóvel {a.Modelo},excluído com sucesso.",
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //método para atualizar Automovel..
        public JsonResult AtualizarAutomovel(AutomovelEdicaoViewModel model)
        {
            try
            {
                //criando um objeto da classe de entidade..
                Automovel a = new Automovel();
                a.IdAutomovel = model.IdAutomovel;
                
                a.Marca = model.Marca;
                a.Modelo = model.Modelo;
                
                a.Placa = model.Placa;
                
                a.KmInicial = model.KmInicial;
                
                a.Foto = model.Foto;
                a.IdMotorista = model.IdMotorista;
                a.IdProprietario = model.IdProprietario;


                AutomovelRepository rep = new AutomovelRepository();
                rep.Update(a);

                return Json($"Automóvel {a.Modelo}, atualizado com sucesso.");
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }


        //método para retornar o relatorio de Automovel..
        public void Relatorio()
        {
            //criando o conteudo do relatorio..
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1 class='titulo'>Relatório dos Automóveis</h1>");
            conteudo.Append("<h4 class='titulo'>VJC TECHNOLOGY</h4>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now} </p>");
            conteudo.Append("<br/>");

            conteudo.Append("<table>");
            conteudo.Append("<tr>");
            conteudo.Append("<th>Foto do Automóvel</th>");
            conteudo.Append("<th>Modelo do Automóvel</th>");
            conteudo.Append("<th>Marca</th>");
            
            conteudo.Append("<th>Placa</th>");
            
            conteudo.Append("<th>Km</th>");
            
            conteudo.Append("<th>Motorista</th>");
            conteudo.Append("<th>Proprietário</th>");
            conteudo.Append("</tr>");

            AutomovelRepository rep = new AutomovelRepository();

            foreach (Automovel a in rep.FindAll())
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td><img src='{a.Foto}' height='60'/></td>");
                conteudo.Append($"<td>{a.Marca}</td>");
                conteudo.Append($"<td>{a.Modelo}</td>");
                
                
                conteudo.Append($"<td>{a.Placa}</td>");
                
                conteudo.Append($"<td>{a.KmInicial}</td>");
                
                conteudo.Append($"<td>{a.Motorista.Nome}</td>");
                conteudo.Append($"<td>{a.Proprietario.Nome}</td>");
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