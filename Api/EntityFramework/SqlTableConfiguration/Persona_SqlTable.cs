using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api.Entity;

namespace Api.EntityFramework.SqlTableConfiguration
{
    public class Persona_SqlTable : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> b)
        {
            b.ToTable(nameof(Persona));

            b.HasKey(p => p.Id);

            b.Property(p => p.Nombres).HasColumnType("nvarchar(50)").IsRequired();

            b.Property(p => p.Apellidos).HasColumnType("nvarchar(50)").IsRequired();


            b.Property(p => p.DocumentoIdentidad).HasColumnType("char(8)").IsRequired();

            b.Property(p => p.FechaNacimiento).HasColumnType("date").IsRequired();

            b.Property(p => p.Estado).HasColumnType("char(1)").IsRequired();

            var data = new List<Persona>() {
                new  () { Id = "1", Nombres = "Juan", Apellidos = "Perez", DocumentoIdentidad = "12345678", FechaNacimiento = DateTime.Now, Estado = "A" },
                new  () { Id = "2", Nombres = "Maria", Apellidos = "Lopez", DocumentoIdentidad = "87654321", FechaNacimiento = DateTime.Now, Estado = "A" },
                new  () { Id = "3", Nombres = "Carlos", Apellidos = "Gomez", DocumentoIdentidad = "45678912", FechaNacimiento = DateTime.Now, Estado = "A" },
                new  () { Id = "4", Nombres = "Luis", Apellidos = "Garcia", DocumentoIdentidad = "78912345", FechaNacimiento = DateTime.Now, Estado = "A" },
            };


            b.HasData(data);



        }
    }
}
