
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Presentation.Controllers
{
    
    public class HomeController : Controller
    {
        // GET: Home
        
        public ActionResult Index()
        {
            return View();
        }


        // GET: Home/Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        // GET: Home/Sobre
        
        public ActionResult Sobre()
        {
            return View();
        }


        // GET: Home/Serviços
        
        public ActionResult Servico()
        {
            return View();
        }


        // GET: Proprietario/Contato
        public ActionResult Contato()
        {
            return View();
        }

    }
}