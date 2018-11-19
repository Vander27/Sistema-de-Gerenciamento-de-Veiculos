using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Presentation.Models
{
    public class CaminhaoEdicaoViewModel
    {
        public int IdCaminhao{ get; set; }
       
        public string Marca { get; set; }
        public string Modelo { get; set; }
        
        public string Placa { get; set; }
        
        public string KmInicial { get; set; }
       
        public string Foto { get; set; }
        public int IdMotorista { get; set; }
        public int IdProprietario { get; set; }
    }
}