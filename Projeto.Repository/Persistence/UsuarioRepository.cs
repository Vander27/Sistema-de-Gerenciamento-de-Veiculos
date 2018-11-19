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
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        public bool HasEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Usuario Find(string email, string v)
        {
            throw new NotImplementedException();
        }
    }
}
