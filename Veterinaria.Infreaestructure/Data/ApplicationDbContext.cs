
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Veterinaria.Domain.Entities;

namespace Veterinaria.Infreaestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Dueno> Duenos => Set<Dueno>();
        public DbSet<Mascota> Mascotas => Set<Mascota>();
        public DbSet<Veterinario> Veterinarios => Set<Veterinario>();
        public DbSet<Cita> Citas => Set<Cita>();

        public DbSet<DetallesCita> detalles => Set<DetallesCita>();
        public DbSet<RefreshToken> RefreshTokens { get; set;  }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Dueño
            builder.Entity<Dueno>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).ValueGeneratedOnAdd();
                entity.Property(d => d.NombreDueno).IsRequired().HasMaxLength(40);
                entity.Property(d => d.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(d => d.DNI).IsRequired().HasMaxLength(25);
                entity.HasIndex(d => d.DNI).IsUnique(); // DNI único
                entity.Property(d => d.FechaRegistro).IsRequired().HasColumnType("date");


                //Relacion con applicationuser
                entity.HasOne(d => d.Usuario)
                     .WithMany()
                     .HasForeignKey(d => d.IdUsuario)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();
            });

            // Mascota
            builder.Entity<Mascota>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Id).ValueGeneratedOnAdd();
                entity.Property(m => m.NombreMascota).IsRequired().HasMaxLength(25);
                entity.Property(m => m.Especie).IsRequired().HasMaxLength(20);
                entity.Property(m => m.Raza).HasMaxLength(20); // Opcional
                entity.Property(m => m.Edad).IsRequired().HasMaxLength(20);
                entity.Property(m => m.Sexo).IsRequired().HasMaxLength(1);
                entity.ToTable(Table =>
                {
                    Table.HasCheckConstraint("CK_Mascota_Sexo", "\"Sexo\" IN('M', 'F')");
                });
                entity.Property(m => m.FechaRegistro).IsRequired().HasColumnType("date");

                // Relación 1 a N con Dueño
                entity.HasOne(m => m.Dueno)
                      .WithMany(d => d.Mascotas)
                      .HasForeignKey(m => m.IdDueno)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();
            });

            // Veterinario
            builder.Entity<Veterinario>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.Id).ValueGeneratedOnAdd();
                entity.Property(v => v.NombreVeterinario).IsRequired().HasMaxLength(40);
                entity.Property(v => v.Especialidad).IsRequired().HasMaxLength(30);
                entity.Property(v => v.DNI).IsRequired().HasMaxLength(25);
                entity.HasIndex(v => v.DNI).IsUnique(); // DNI único
                entity.Property(v => v.Telefono).IsRequired().HasMaxLength(20);
                entity.Property(v => v.FechaRegistro).IsRequired().HasColumnType("date");

                //Relacion con applicationuser
                entity.HasOne(d => d.Usuario)
                     .WithMany()
                     .HasForeignKey(d => d.IdUsuario)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();
            });

            // Cita
            builder.Entity<Cita>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Motivo).IsRequired().HasMaxLength(250);
                //entity.Property(c => c.Estado).IsRequired().HasDefaultValue(true);
                entity.Property(c => c.Estado).IsRequired().HasMaxLength(8);
                entity.ToTable(Table =>
                {
                    Table.HasCheckConstraint("CK_Cita_Estado", "\"Estado\" IN('Activo', 'Inactivo')");
                });


                // FechaCita guarda fecha y hora
                entity.Property(c => c.FechaCita).IsRequired().HasColumnType("timestamp");
                entity.HasIndex(c => c.FechaCita)
                .IsUnique();
                entity.Property(c => c.FechaRegistro).IsRequired().HasColumnType("date");

                //Relación con Duenño
                entity.HasOne(c => c.Dueno)
                      .WithMany(d => d.Citas)
                      .HasForeignKey(c => c.IdDueno)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

                // Relación  con Mascota
                //entity.HasOne(c => c.Mascota)
                //      .WithMany(m => m.Citas)
                //      .HasForeignKey(c => c.IdMascota)
                //      .OnDelete(DeleteBehavior.Restrict)
                //      .IsRequired();

                // Relación con Veterinario
                entity.HasOne(c => c.Veterinario)
                      .WithMany(v => v.Citas)
                      .HasForeignKey(c => c.IdVeterinario)
                      .OnDelete(DeleteBehavior.Restrict)
                      .IsRequired();

                //Relacion con applicationuser
                entity.HasOne(c => c.Usuario)
                     .WithMany()
                     .HasForeignKey(c => c.IdUsuario)
                     .OnDelete(DeleteBehavior.Restrict)
                     .IsRequired();

            });

            //Detalles cita
            builder.Entity<DetallesCita>(entity =>
            {

            entity.HasKey(d => d.Id);
            entity.Property(d => d.Id).ValueGeneratedOnAdd();
            entity.Property(d => d.NombreMascota).IsRequired().HasMaxLength(25);

            // Relación  con Mascota
            entity.HasOne(d => d.Mascota)
                  .WithMany(m => m.Detalles)
                  .HasForeignKey(c => c.MacotaId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired();

            // Relación  con Citas
            entity.HasOne(c => c.Cita)
                  .WithMany(m => m.Detalles)
                  .HasForeignKey(c => c.CitaID)
                  .OnDelete(DeleteBehavior.Restrict)
                  .IsRequired();

            });

            builder.Entity<ApplicationUser>(static entity =>
            {
                entity.Property(u => u.NombreCompleto)
                .IsRequired()
                .HasMaxLength(75);
            });
        }
    }
}
