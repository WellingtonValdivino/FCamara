using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(task => task.Id);

            entity.Property(task => task.Title)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(task => task.Description)
                .HasMaxLength(500);

            entity.Property(task => task.Status)
                .IsRequired();

            entity.Property(task => task.CreatedAt)
                .IsRequired();

            entity.Property(task => task.UpdatedAt)
                .IsRequired(false);

            entity.Property(task => task.DueDate)
                .IsRequired(false);
        });
    }
}