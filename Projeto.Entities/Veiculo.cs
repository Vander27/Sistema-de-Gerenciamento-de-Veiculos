using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class Veiculo
    {
        public int IdVeiculo { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Placa { get; set; }
        public string Foto { get; set; }

        //chaves estrangeiras..
        public int IdMotorista { get; set; }
        public int IdProprietario { get; set; }

        //relacionamentos
        public Motorista Motorista { get; set; }
        public Proprietario Proprietario { get; set; }

        public Veiculo()
        {

        }

        public Veiculo(int idVeiculo, string modelo, string marca, string placa, string foto)
        {
            IdVeiculo = idVeiculo;
            Modelo = modelo;
            Marca = marca;
            Placa = placa;
            Foto = foto;
        }

        
        public override string ToString()
        {
            return $"Id: {IdVeiculo}, Modelo: {Modelo}, Marca: {Marca}, Placa: {Placa}, Foto: {Foto}";

        }
    }
}
