using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class Caminhao
    {
        public int IdCaminhao { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string KmInicial { get; set; }
        public string Foto { get; set; }

        //chaves estrangeiras..
        public int IdMotorista { get; set; }
        public int IdProprietario { get; set; }

        //relacionamentos
        public Motorista Motorista { get; set; }
        public Proprietario Proprietario { get; set; }

        public Caminhao()
        {

        }

        public Caminhao(int idCaminhao, string marca, string modelo, string placa, string kmInicial, string foto)
        {
            IdCaminhao = idCaminhao;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            KmInicial = kmInicial;
            Foto = foto;
        }

        public override string ToString()
        {
            return $"Id: {IdCaminhao}, Marca: {Marca}, Modelo: {Modelo}, Placa: {Placa},  KmInicial: {KmInicial}, Foto: {Foto}";

        }
    }
}
