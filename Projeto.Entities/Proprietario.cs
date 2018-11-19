using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class Proprietario
    {
        public int IdProprietario { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        public List<Automovel> Automoveis { get; set; }
        public List<Caminhao> Caminhoes { get; set; }

        public Proprietario()
        {

        }

        public Proprietario(int idProprietario, string nome, string cnpj)
        {
            IdProprietario = idProprietario;
            Nome = nome;
            Cnpj = cnpj;
        }

        public override string ToString()
        {
            return $"Id: {IdProprietario}, Nome: {Nome}, Cnpj: {Cnpj}";
        }
    }
}
