using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class ProprietarioCadastroViewModel
    {

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caraxcteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome da Empresa.")]
        public string Nome { get; set; }

        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caraxcteres.")]
        [MaxLength(50, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o cnpj da Empresa.")]
        public string Cnpj { get; set; }
    }
}