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
        public int QtdAutomoveis(int idProprietario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Automovel
                    .Where(a => a.IdProprietario == idProprietario)
                    .Count();
            }
        }

        public int QtdCaminhoes(int idProprietario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Caminhao
                    .Where(c => c.IdProprietario == idProprietario)
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
