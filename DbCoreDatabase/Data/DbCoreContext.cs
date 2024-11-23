using System;
using System.Collections.Generic;
using DbCoreDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace DbCoreDatabase.Data;

public partial class DbCoreContext : DbContext
{
    public DbCoreContext()
    {
    }

    public DbCoreContext(DbContextOptions<DbCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DbAcess> DbAcesses { get; set; }

    public virtual DbSet<DbTable> DbTables { get; set; }

    public virtual DbSet<Dbasis> Dbases { get; set; }

    public virtual DbSet<TableColumn> TableColumns { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=DbCore;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DbAcess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DBAcesse__3214EC07135BA4F5");
        });

        modelBuilder.Entity<DbTable>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DbTables__3214EC07859977B4");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Dbasis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DBases__3214EC0778F52263");

            entity.ToTable("DBases");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<TableColumn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TableCol__3214EC075A36010A");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Type).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07F51105F1");

            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Pass).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
