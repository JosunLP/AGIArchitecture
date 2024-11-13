
using System;
using Microsoft.EntityFrameworkCore;

namespace AGI.DataManagement;

public class KnowledgeGraphContext(DbContextOptions<KnowledgeGraphContext> options) : DbContext(options)
{
    public required DbSet<KnowledgeNode> KnowledgeNodes { get; set; }
    public required DbSet<Experience> Experiences { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use an in-memory database for simplicity; replace with SQL Server or other DB if needed.
            optionsBuilder.UseInMemoryDatabase("KnowledgeGraphDB");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships and constraints
        modelBuilder.Entity<KnowledgeNode>().HasKey(k => k.Id);
        modelBuilder.Entity<Experience>().HasKey(e => e.Id);
    }
}

