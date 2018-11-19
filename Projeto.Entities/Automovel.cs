using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class Automovel
    {
        public int IdAutomovel { get; set; }
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

        public Automovel()
        {

        }

        public Automovel(int idAutomovel, string marca, string modelo, string placa, string kmInicial, string foto)
        {
            IdAutomovel = idAutomovel;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
            KmInicial = kmInicial;
            Foto = foto;
        }

        public override string ToString()
        {
            return $"Id: {IdAutomovel}, Marca: {Marca}, Modelo: {Modelo}, Placa: {Placa}, KmInicial: {KmInicial}, Foto: {Foto}";

        }
    }
}
