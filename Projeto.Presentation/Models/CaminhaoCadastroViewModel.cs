using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class CaminhaoCadastroViewModel
    {
       

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a marca do caminhão.")]
        public string Marca { get; set; }

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o modelo do caminhão.")]
        public string Modelo { get; set; }

        


        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a placa do caminhão.")]
        public string Placa { get; set; }

       

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o Km  do caminhão.")]
        public string KmInicial { get; set; }

        

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(500, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a foto do Caminhão.")]
        public string Foto { get; set; }


        [Required(ErrorMessage = "Selecione o motorista do caminhão.")]
        public int IdMotorista { get; set; }


        [Required(ErrorMessage = "Selecione a Empresa do caminhão.")]
        public int IdProprietario { get; set; }
    }
}