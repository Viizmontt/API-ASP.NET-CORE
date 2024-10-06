using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI002.Models;

public partial class Test003Context : DbContext
{
    public Test003Context()
    {
    }

    public Test003Context(DbContextOptions<Test003Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__area__3213E83FD47DCE3A");

            entity.ToTable("area");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__empleado__3213E83F299C9CC4");

            entity.ToTable("empleados");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AreaId).HasColumnName("areaId");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fechaNacimiento");
            entity.Property(e => e.JefeInmediatoId).HasColumnName("jefeInmediatoId");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("salario");

            entity.HasOne(d => d.Area).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__empleados__areaI__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
