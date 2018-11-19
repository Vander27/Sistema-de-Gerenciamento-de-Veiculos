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
   public class AutomovelRepository : GenericRepository<Automovel>
    {
        public override List<Automovel> FindAll()
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Automovel
                    .Include(a => a.Motorista)  //JOIN
                    .Include(a => a.Proprietario) //JOIN
                    .ToList();
            }
        }
    }
}
