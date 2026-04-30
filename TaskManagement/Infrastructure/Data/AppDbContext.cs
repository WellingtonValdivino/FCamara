using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data;

/// <summary>
/// Contexto da aplicação para acesso ao banco de dados, utilizando Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Construtor do contexto, recebe as opções de configuração do banco de dados.
    /// </summary>
    /// <param name="options">Opções de configuração do contexto.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Define o DbSet para a entidade TaskItem, permitindo operações de CRUD no banco de dados.
    /// </summary>
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    /// <summary>
    /// Configura a entidade TaskItem, definindo chaves primárias, propriedades e restrições para o modelo de dados.
    /// </summary>
    /// <param name="modelBuilder"></param>
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