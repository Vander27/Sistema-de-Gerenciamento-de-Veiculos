using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Motorista
    {
        public int IdMotorista { get; set; }
        public string Nome { get; set; }

        //relacionamento
        public List<Veiculo> Veiculos { get; set; }

        public Motorista()
        {

        }

        public Motorista(int idMotorista, string nome)
        {
            IdMotorista = idMotorista;
            Nome = nome;
        }

        public override string ToString()
        {
            return $"Id: {IdMotorista}, Nome: {Nome}";
        }
    }
}
