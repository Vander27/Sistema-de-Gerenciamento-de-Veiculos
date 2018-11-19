using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Entity;
using Projeto.Repository.Mappings;
using Projeto.Entities;

namespace Projeto.Repository.Context
{
    //Regra 1) Esta classe deverá Herdar 'DbContext'..
    public class DataContext : DbContext
    {
        public DataContext()
            : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {

        }
        //sobrescrita do método OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MotoristaMap());
            modelBuilder.Configurations.Add(new ProprietarioMap());
            modelBuilder.Configurations.Add(new AutomovelMap());
            modelBuilder.Configurations.Add(new CaminhaoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());


        }

        public DbSet<Motorista> Motorista { get; set; }
        public DbSet<Automovel> Automovel { get; set; }
        public DbSet<Caminhao> Caminhao { get; set; }
        public DbSet<Proprietario> Proprietario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
