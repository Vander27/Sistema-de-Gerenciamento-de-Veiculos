using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class Usuario
    {
        //propriedades [prop] + 2x[tab]
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        

        //construtor default [ctor] + 2x[tab]
        public Usuario()
        {
            //vazio
        }

        //sobrecarga de construtores (overloading)
        public Usuario(int idUsuario, string nome, string email,
               string senha)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
           
        }

        //sobrescrita do método ToString()
        public override string ToString()
        {
            return $"Id: {IdUsuario}, Nome: {Nome}, Email: { Email}, Senha: { Senha}, ";
        }


    }
}
