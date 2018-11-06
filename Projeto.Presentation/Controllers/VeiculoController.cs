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
    public class VeiculoController : Controller
    {
        // GET: Veiculo/Cadastro..
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: Veiculo/Consulta..
        public ActionResult Consulta()
        {
            return View();
        }

        //JsonResult -> Receber chamadas AJAX (JavaScript)..
        public JsonResult CadastrarVeiculo(VeiculoCadastroViewModel model)
        {
            //verificar se os dados da model passaram nas validações..
            if (ModelState.IsValid)
            {
                try
                {
                    Veiculo v = new Veiculo();
                    v.Modelo = model.Modelo;
                    v.Marca = model.Marca;
                    v.Placa = model.Placa;
                    v.Foto = model.Foto;
                    v.IdMotorista = model.IdMotorista;
                    v.IdProprietario = model.IdProprietario;

                    //gravar no banco..
                    VeiculoRepository rep = new VeiculoRepository();
                    rep.Insert(v);
                    return Json($"Veiculo {v.Modelo}, cadastrado com sucesso.");

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


        //método para retornar a consulta de veiculo para o Angular..
        public JsonResult ConsultarVeiculos()
        {
            try
            {
                //declarar uma lista da classe VeiculoConsultaViewModel..
                List<VeiculoConsultaViewModel> lista = new List<VeiculoConsultaViewModel>();

                //varrer cada veiculo obtido do banco de dados
                VeiculoRepository rep = new VeiculoRepository();
                foreach (Veiculo v in rep.FindAll())
                {
                    VeiculoConsultaViewModel model = new VeiculoConsultaViewModel();
                    
                    model.IdVeiculo = v.IdVeiculo;
                    model.Modelo = v.Modelo;
                    model.Marca = v.Marca;
                    model.Placa = v.Placa;
                    model.Foto = v.Foto;
                    model.IdMotorista = v.IdMotorista;
                    model.NomeMotorista = v.Motorista.Nome;
                    model.IdProprietario = v.IdProprietario;
                    model.NomeProprietario = v.Proprietario.Nome;

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


        //método  para retornar 1 veículo pelo id..
        public JsonResult ObterVeiculo(int idVeiculo)
        {
            try
            {
                //buscar 1 veículo no banco de dados pelo id..
                VeiculoRepository rep = new VeiculoRepository();
                Veiculo v = rep.FindById(idVeiculo);

                //retornando para a página..
                VeiculoConsultaViewModel model = new VeiculoConsultaViewModel();
                model.IdVeiculo = v.IdVeiculo;
                model.Modelo = v.Modelo;
                model.Marca = v.Marca;
                model.Placa = v.Placa;
                model.Foto = v.Foto;
                model.IdMotorista = v.IdMotorista;
                model.IdProprietario = v.IdProprietario;

                //enviando para a página..
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //retornar mensagem de erro..
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }


        //método para excluir um  veículo..
        public JsonResult ExcluirVeiculo(int idVeiculo)
        {
            try
            {
                //buscar o veículo na base de dados pelo id..
                VeiculoRepository rep = new VeiculoRepository();
                Veiculo v = rep.FindById(idVeiculo);

                //excluindo o veiculo
                rep.Delete(v);

                //retornando mensagem de sucesso..
                return Json($"Veiculo {v.Modelo},excluído com sucesso.",
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        //método para atualizar veiculo..
        public JsonResult AtualizarVeiculo(VeiculoEdicaoViewModel model)
        {
            try
            {
                //criando um objeto da classe de entidade..
                Veiculo v = new Veiculo();
                v.IdVeiculo = model.IdVeiculo;
                v.Modelo = model.Modelo;
                v.Marca = model.Marca;
                v.Placa = model.Placa;
                v.IdMotorista = model.IdMotorista;
                v.IdProprietario = model.IdProprietario;


                VeiculoRepository rep = new VeiculoRepository();
                rep.Update(v);

                return Json($"Veiculo {v.Modelo}, atualizado com sucesso.");
            }
            catch (Exception e)
            {

                return Json(e.Message);
            }
        }


        //método para retornar o relatorio de Veiculos..
        public void Relatorio()
        {
            //criando o conteudo do relatorio..
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1 class='titulo'>Relatório da Frota de Veículos</h1>");
            conteudo.Append("<h4 class='titulo'>VJC TECHNOLOGY</h4>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now} </p>");
            conteudo.Append("<br/>");

            conteudo.Append("<table>");
            conteudo.Append("<tr>");
            conteudo.Append("<th>Foto do Veiculo</th>");
            conteudo.Append("<th>Modelo do Veículo</th>");
            conteudo.Append("<th>Marca</th>");
            conteudo.Append("<th>Motorista</th>");
            conteudo.Append("<th>Proprietário</th>");
            conteudo.Append("</tr>");

            VeiculoRepository rep = new VeiculoRepository();

            foreach (Veiculo v in rep.FindAll())
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td><img src='{v.Foto}' height='60'/></td>");
                conteudo.Append($"<td>{v.Modelo}</td>");
                conteudo.Append($"<td>{v.Marca}</td>");
                conteudo.Append($"<td>{v.Motorista.Nome}</td>");
                conteudo.Append($"<td>{v.Proprietario.Nome}</td>");
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