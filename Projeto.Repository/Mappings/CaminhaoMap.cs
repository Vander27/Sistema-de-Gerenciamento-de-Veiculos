using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
   public class CaminhaoMap : EntityTypeConfiguration<Caminhao>
    {

        public CaminhaoMap()
        {

            //nome da tabela..
            ToTable("Caminhao");

            //chave primário..
            HasKey(c => c.IdCaminhao);

            //mapear os demais campos da tabela
            Property(c => c.IdCaminhao)
                .HasColumnName("IdCaminhao")
                .IsRequired();

            Property(c => c.Marca)
              .HasColumnName("Marca")
              .HasMaxLength(50)
              .IsRequired();

            Property(c => c.Modelo)
               .HasColumnName("Modelo")
               .HasMaxLength(50)
               .IsRequired();


           


            Property(c => c.Placa)
               .HasColumnName("Placa")
               .HasMaxLength(50)
               .IsRequired();


           

            Property(c => c.KmInicial)
               .HasColumnName("Km")
               .HasMaxLength(50)
               .IsRequired();



            Property(c => c.Foto)
              .HasColumnName("Foto")
              .HasMaxLength(500)
              .IsRequired();

            Property(c => c.IdMotorista)
              .HasColumnName("IdMotorista")
              .IsRequired();

            Property(c => c.IdProprietario)
              .HasColumnName("IdProprietario")
              .IsRequired();

            //Mapear a chave estrangeira com a..
            //tabela do Motorista..

            HasRequired(c => c.Motorista) // Veiculo tem 1 Motorista..
                .WithMany(m => m.Caminhoes) // Motorista tem muitos Caminhao..
                .HasForeignKey(c => c.IdMotorista)
                .WillCascadeOnDelete(false);//Chave estrangeira


            //Mapear a chave estrangeira com a..
            //tabela do Proprietario..

            HasRequired(c => c.Proprietario) // Caminhao tem 1 Proprietario..
               .WithMany(p => p.Caminhoes) // Proprietario tem muitos Automovel..
               .HasForeignKey(c => c.IdProprietario)
               .WillCascadeOnDelete(false);//Chave estrangeira

        }

    }
}
