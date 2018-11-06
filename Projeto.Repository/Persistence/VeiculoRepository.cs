using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.Repository.Generics;
using Projeto.Repository.Context;
using System.Data.Entity;

namespace Projeto.Repository.Persistence
{
   public class VeiculoRepository : GenericRepository<Veiculo>
    {
        public override List<Veiculo> FindAll()
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Veiculo
                    .Include(v => v.Motorista)  //JOIN
                    .Include(v => v.Proprietario) //JOIN
                    .ToList();
            }
        }
    }
}
