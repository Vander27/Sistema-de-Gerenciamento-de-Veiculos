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
        public string Cpf { get; set; }
        public string Telefone { get; set; }

        //relacionamento
        public List<Automovel> Automovels { get; set; }
        public List<Caminhao> Caminhoes { get; set; }
        public ICollection<Automovel> Automoveis { get; set; }

        public Motorista()
        {

        }

        public Motorista(int idMotorista, string nome, string cpf, string telefone)
        {
            IdMotorista = idMotorista;
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return $"Id: {IdMotorista}, Nome: {Nome}, Cpf: {Cpf}, Telefone: {Telefone}";
        }
    }
}
