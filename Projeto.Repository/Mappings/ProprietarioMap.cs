using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;


namespace Projeto.Repository.Mappings
{
    public class ProprietarioMap : EntityTypeConfiguration<Proprietario>
    {
        public ProprietarioMap()
        {
            //nome da tabela..
            ToTable("Proprietario");

            //chave primário..
            HasKey(p => p.IdProprietario);

            //mapear os demais campos da tabela
            Property(p => p.IdProprietario)
                .HasColumnName("IdProprietario")
                .IsRequired();

            Property(p => p.Nome)
               .HasColumnName("Nome")
               .HasMaxLength(50)
               .IsRequired();

            Property(p => p.Cnpj)
               .HasColumnName("Cnpj")
               .HasMaxLength(50)
               .IsRequired();


        }
    }
}
