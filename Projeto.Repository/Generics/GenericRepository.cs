using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Projeto.Repository.Context;

namespace Projeto.Repository.Generics
{
    //<T> Tipo de generico
   public class GenericRepository<T>
        where T : class
    {
        //método para inserir um registro na base..
        public virtual void Insert(T obj)
       
        {
            //acessar o banco de dados com o EntityFramework..
            using (DataContext ctx = new DataContext())
            {
                ctx.Entry(obj).State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        //método para atualizar um registro na base..
        public virtual void Update(T obj)
        {
            //acessar o banco de dados com o EntityFramework..
            using (DataContext ctx = new DataContext())
            {
                ctx.Entry(obj).State = EntityState.Modified;
                ctx.SaveChanges();

            }
        }

        //método para excluir um registro na base..
        public virtual void Delete(T obj)
        {
            //acessar o banco de dados com o EntityFramework..
            using (DataContext ctx = new DataContext())
            {
                ctx.Entry(obj).State = EntityState.Deleted;
                ctx.SaveChanges();
            }

        }

        //método para retornar toto o registro na base;;
        public virtual List<T> FindAll()
        {
            //acessa o banco de dados com o EntityFramework..
            using (DataContext ctx = new DataContext())
            {
                return ctx.Set<T>().ToList();
            }
        }

        //método para retornar 1 registro pelo id..
        public virtual T FindById(int id)
        {
            //acessar o banco de dados com o Entityframework..
            using (DataContext ctx = new DataContext())
            {
                return ctx.Set<T>().Find(id);
            }
        }
        
    
    }
}
