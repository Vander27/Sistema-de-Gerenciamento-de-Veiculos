using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
   public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            //nome da tabela..
            ToTable("Usuario");

            //chave primário..
            HasKey(u => u.IdUsuario);

            //mapear os demais campos da tabela
            Property(u => u.IdUsuario)
                .HasColumnName("IdUsuario")
                .IsRequired();

            Property(u => u.Nome)
               .HasColumnName("Nome")
               .HasMaxLength(100)
               .IsRequired();

            Property(u => u.Email)
               .HasColumnName("Email")
               .IsRequired();

            Property(u => u.Senha)
               .HasColumnName("Senha")
               .HasMaxLength(50)
               .IsRequired();

          


        }
    }
}
