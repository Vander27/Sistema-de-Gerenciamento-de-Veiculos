using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.Repository.Generics;
using Projeto.Repository.Context;

namespace Projeto.Repository.Persistence
{
   public class MotoristaRepository : GenericRepository<Motorista>
    {
        //método que retorne a quantidade de Automoveis..
        //de um determinado Motorista..
        public int QtdAutomoveis(int idMotorista)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Automovel
                    .Where(a => a.IdMotorista == idMotorista)
                    .Count();
            }
        }

        //método que retorne a quantidade de Caminhoes..
        //de um determinado Motorista..
        public int QtdCaminhoes(int idMotorista)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Caminhao
                    .Where(c => c.IdMotorista == idMotorista)
                    .Count();
            }
        }

        public int QtdVeiculos(int idMotorista)
        {
            throw new NotImplementedException();
        }

        public int QtdCaminhoes(object idCaminhao)
        {
            throw new NotImplementedException();
        }
    }
}
