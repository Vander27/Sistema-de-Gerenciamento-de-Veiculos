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

        public List<Veiculo> Veiculos { get; set; }

        public Proprietario()
        {

        }

        public Proprietario(int idProprietario, string nome)
        {
            IdProprietario = idProprietario;
            Nome = nome;
        }

        public override string ToString()
        {
            return $"Id: {IdProprietario}, Nome: {Nome}";
        }
    }
}
