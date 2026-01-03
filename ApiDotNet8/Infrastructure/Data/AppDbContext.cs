using Microsoft.EntityFrameworkCore;
using ApiDotNet8.Domain.Entities;

namespace ApiDotNet8.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title)
                  .IsRequired()
                  .HasMaxLength(200);
        });
    }
}
