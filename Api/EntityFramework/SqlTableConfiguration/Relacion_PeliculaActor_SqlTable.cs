using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalApiPeliculas.Entity;

namespace MinimalApiPeliculas.EntityFramework.SqlTableConfiguration
{
    public class Relacion_PeliculaActor_SqlTable : IEntityTypeConfiguration<Relacion_PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<Relacion_PeliculaActor> b)
        {
            b.ToTable(nameof(Relacion_PeliculaActor));

            b.HasKey(p => new { p.Pelicula_Id,p.Actor_Id});

            b.HasOne(p => p.Actor).WithMany(p => p.Relacion_PeliculaActor).HasForeignKey(p => p.Actor_Id);
           
            b.HasOne(p => p.Pelicula).WithMany(p => p.Relacion_PeliculaActor).HasForeignKey(p => p.Pelicula_Id);
        }
    }
}
