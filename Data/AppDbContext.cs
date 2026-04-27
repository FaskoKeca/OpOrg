using Microsoft.EntityFrameworkCore;
using OpOrg.Models;

namespace OpOrg.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Operation> Operations => Set<Operation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasIndex(d => d.UIN).IsUnique();

            entity.HasMany(d => d.Operations)
                .WithOne()
                .HasForeignKey(o => o.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasIndex(p => p.EGN).IsUnique();

            entity.HasMany(p => p.Operations)
                .WithOne()
                .HasForeignKey(o => o.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(o => o.Description)
                .HasMaxLength(500)
                .IsRequired();

            entity.Property(o => o.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.Property(o => o.DateTime)
                .IsRequired();
        });
    }
}
