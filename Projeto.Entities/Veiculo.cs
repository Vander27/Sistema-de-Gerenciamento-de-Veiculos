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
        public int Ano { get; set; }
        public string Placa { get; set; }
        public string Cor { get; set; }
        public int KmInicial { get; set; }
        public int KmFinal { get; set; }
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

        public Veiculo(int idVeiculo, string modelo, string marca, int ano, string placa, string cor, int kmInicial, int kmFinal, string foto)
        {
            IdVeiculo = idVeiculo;
            Modelo = modelo;
            Marca = marca;
            Ano = ano;
            Placa = placa;
            Cor = cor;
            KmInicial = kmInicial;
            KmFinal = kmFinal;
            Foto = foto;
        }

        public override string ToString()
        {
            return $"Id: {IdVeiculo}, Modelo: {Modelo}, Marca: {Marca}, Ano: {Ano}, Pçaca: {Placa}, Cor: {Cor}, KmInicial: {KmInicial}, KmFinal: {KmFinal}";

        }
    }
}
