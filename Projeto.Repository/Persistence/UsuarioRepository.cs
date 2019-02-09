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
            using (DataContext ctx = new DataContext())
            {
                return ctx.Usuario
                          .Count(u => u.Email.Equals(email)) > 0;
            }
        }


        public Usuario Obter(string email, string senha)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Usuario
                          .FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            }
        }
    }
}
