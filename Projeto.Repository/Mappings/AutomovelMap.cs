using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
    public class AutomovelMap : EntityTypeConfiguration<Automovel>
    {
        public AutomovelMap()
        {

            //nome da tabela..
            ToTable("Automovel");

            //chave primário..
            HasKey(a => a.IdAutomovel);

            //mapear os demais campos da tabela
            Property(a => a.IdAutomovel)
                .HasColumnName("IdAutomovel")
                .IsRequired();

            Property(a => a.Marca)
               .HasColumnName("Marca")
               .HasMaxLength(50)
               .IsRequired();


            Property(a => a.Modelo)
               .HasColumnName("Modelo")
               .HasMaxLength(50)
               .IsRequired();

            



            Property(a => a.Placa)
               .HasColumnName("Placa")
               .HasMaxLength(50)
               .IsRequired();

           

            Property(a => a.KmInicial)
               .HasColumnName("Km")
               .HasMaxLength(50)
               .IsRequired();

           

            Property(a => a.Foto)
              .HasColumnName("Foto")
              .HasMaxLength(500)
              .IsRequired();

            Property(a => a.IdMotorista)
              .HasColumnName("IdMotorista")
              .IsRequired();

            Property(a => a.IdProprietario)
              .HasColumnName("IdProprietario")
              .IsRequired();

            //Mapear a chave estrangeira com a..
            //tabela do Motorista..

            HasRequired(a => a.Motorista) // Veiculo tem 1 Motorista..
                .WithMany(m => m.Automoveis) // Motorista tem muitos Automovel..
                .HasForeignKey(a => a.IdMotorista)
                .WillCascadeOnDelete(false);//Chave estrangeira


            //Mapear a chave estrangeira com a..
            //tabela do Proprietario..

            HasRequired(a => a.Proprietario) // Veiculo tem 1 Proprietario..
               .WithMany(p => p.Automoveis) // Proprietario tem muitos Automovel..
               .HasForeignKey(a => a.IdProprietario)
               .WillCascadeOnDelete(false);//Chave estrangeira

        }       
    }
}
