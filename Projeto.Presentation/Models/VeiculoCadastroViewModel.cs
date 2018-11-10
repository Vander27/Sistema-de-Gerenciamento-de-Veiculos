using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class VeiculoCadastroViewModel
    {
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o modelo do veículo.")]
        public string Modelo { get; set; }

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a marca do veículo.")]
        public string Marca { get; set; }

        
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(50,ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a placa do veículo.")]
        public string Placa { get; set; }


        
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(500, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a foto do veículo.")]
        public string Foto { get; set; }

       
        [Required(ErrorMessage = "Selecione o motorista do veículo.")]
        public int IdMotorista { get; set; }

    
        [Required(ErrorMessage = "Selecione a Empresa do veículo.")]
        public int IdProprietario { get; set; }
    }
}