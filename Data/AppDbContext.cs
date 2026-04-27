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
    public DbSet<Consultation> Consultations => Set<Consultation>();
    public DbSet<Event> Events => Set<Event>();

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
            entity.HasMany(p => p.Operations)
                .WithOne()
                .HasForeignKey(o => o.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.Events)
                .WithOne()
                .HasForeignKey(e => e.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Operation>(entity =>
        {
            entity.Property(o => o.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(o => o.Notes)
                .HasMaxLength(1000);

            entity.Property(o => o.Price)
                .IsRequired();

            entity.Property(o => o.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasMany(o => o.Consultations)
                .WithOne(c => c.Operation)
                .HasForeignKey(c => c.OperationId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(o => o.DateTime)
                .IsRequired();
        });
    }
}
