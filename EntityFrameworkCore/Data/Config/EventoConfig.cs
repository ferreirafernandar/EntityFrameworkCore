using EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Config
{
    public class EventoConfig : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder
                .Property(e => e.Id)
                .HasColumnName("EventoId");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Descricao)
                .HasColumnType("varchar(500)")
                .HasMaxLength(500);

            builder
                .ToTable("Eventos");
        }
    }
}
