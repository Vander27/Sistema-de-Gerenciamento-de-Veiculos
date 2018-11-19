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
   public class CaminhaoRepository : GenericRepository<Caminhao>
    {
        public override List<Caminhao> FindAll()
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Caminhao
                    .Include(c => c.Motorista)  //JOIN
                    .Include(c => c.Proprietario) //JOIN
                    .ToList();
            }
        }

    }
}
