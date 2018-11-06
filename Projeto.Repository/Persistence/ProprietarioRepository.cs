using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.Repository.Context;
using Projeto.Repository.Generics;

namespace Projeto.Repository.Persistence
{
   public class ProprietarioRepository : GenericRepository<Proprietario>
    {
        public int QtdVeiculos(int idProprietario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Veiculo
                    .Where(v => v.IdProprietario == idProprietario)
                    .Count();
            }
        }
    }
}
