using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class UsuarioAutenticarViewModel
    {
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha de acesso.")]
        public string Senha { get; set; }

    }
}