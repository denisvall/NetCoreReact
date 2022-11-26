using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NetCoreReact.Models;

public partial class DbdemosContext : DbContext
{
    public DbdemosContext()
    {
    }

    public DbdemosContext(DbContextOptions<DbdemosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DetTarea> DetTareas { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DV-WORK\\SQL_2019_DEV; Initial Catalog=DBDemos; Persist Security Info=True; User Id=sa; Password=joseD2017; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DetTarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_detTarea_ID");

            entity.ToTable("detTarea", "react");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Descrpcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persons__3214EC27D5DD6B45");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
