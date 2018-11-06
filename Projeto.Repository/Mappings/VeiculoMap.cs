using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
    public class VeiculoMap : EntityTypeConfiguration<Veiculo>
    {
        public VeiculoMap()
        {

            //nome da tabela..
            ToTable("Veiculo");

            //chave primário..
            HasKey(v => v.IdVeiculo);

            //mapear os demais campos da tabela
            Property(v => v.IdVeiculo)
                .HasColumnName("IdVeiculo")
                .IsRequired();

            Property(v => v.Modelo)
               .HasColumnName("Modelo")
               .HasMaxLength(50)
               .IsRequired();

            Property(v => v.Marca)
               .HasColumnName("Marca")
               .HasMaxLength(50)
               .IsRequired();


            Property(v => v.Placa)
               .HasColumnName("Placa")
               .HasMaxLength(50)
               .IsRequired();

            Property(v => v.Foto)
              .HasColumnName("Foto")
              .HasMaxLength(500)
              .IsRequired();

            Property(v => v.IdMotorista)
              .HasColumnName("IdMotorista")
              .IsRequired();

            Property(v => v.IdProprietario)
              .HasColumnName("IdProprietario")
              .IsRequired();

            //Mapear a chave estrangeira com a..
            //tabela do Motorista..

            HasRequired(v => v.Motorista) // Veiculo tem 1 Motorista..
                .WithMany(m => m.Veiculos) // Motorista tem muitos Veiculos..
                .HasForeignKey(v => v.IdMotorista)
                .WillCascadeOnDelete(false);//Chave estrangeira


            //Mapear a chave estrangeira com a..
            //tabela do Proprietario..

            HasRequired(v => v.Proprietario) // Veiculo tem 1 Proprietario..
               .WithMany(p => p.Veiculos) // Proprietario tem muitos Veiculos..
               .HasForeignKey(v => v.IdProprietario)
               .WillCascadeOnDelete(false);//Chave estrangeira

        }       
    }
}
