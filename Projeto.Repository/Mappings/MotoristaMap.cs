using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
    public class MotoristaMap : EntityTypeConfiguration<Motorista>
    {
        public MotoristaMap()
        {
            //nome da tabela..
            ToTable("Motorista");

            //chave primário..
            HasKey(m => m.IdMotorista);

            //mapear os demais campos da tabela
            Property(m => m.IdMotorista)
                .HasColumnName("IdMotorista")
                .IsRequired();

            Property(m => m.Nome)
               .HasColumnName("Nome")
               .HasMaxLength(50)
               .IsRequired();


        }
    }
}
