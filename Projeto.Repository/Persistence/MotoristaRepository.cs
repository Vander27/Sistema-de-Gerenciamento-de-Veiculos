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
        //método que retorne a quantidade de Veiculos..
        //de um determinado Motorista..
        public int QtdVeiculos(int idMotorista)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Veiculo
                    .Where(v => v.IdMotorista == idMotorista)
                    .Count();
            }
        }
    }
}
