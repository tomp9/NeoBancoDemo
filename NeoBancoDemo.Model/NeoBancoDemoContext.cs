using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NeoBancoDemo.Models;

public partial class NeoBancoDemoContext : DbContext
{

    public NeoBancoDemoContext(DbContextOptions<NeoBancoDemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Cuenta> Cuenta { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-SB693B4T\\SQLEXPRESS;Database=NeoBancoDemo;Trusted_Connection=True;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente");

            entity.HasIndex(e => e.PersonaId, "IX_Cliente_persona_id")
                .IsUnique();

            entity.Property(e => e.ClienteId)
                .ValueGeneratedNever()
                .HasColumnName("cliente_id");

            entity.Property(e => e.Contrasena)
                .HasColumnType("numeric(4, 0)")
                .HasColumnName("contrasena");

            entity.Property(e => e.Estado).HasColumnName("estado");

            entity.Property(e => e.PersonaId).HasColumnName("persona_id");

            //entity.HasOne(d => d.Persona)
            //    .WithOne(p => p.Cliente)
            //    .HasForeignKey<Cliente>(d => d.PersonaId)
            //    .HasConstraintName("FK_Cliente_Persona");
        });


        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.CuentaId).HasName("PK_Cuenta_cuenta_id");

            entity.HasIndex(e => e.NumCuenta, "IX_Cuenta_num_cuenta").IsUnique();

            entity.Property(e => e.CuentaId)
                .ValueGeneratedNever()
                .HasColumnName("cuenta_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NumCuenta)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("num_cuenta");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("saldo_inicial");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_cuenta");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Cuenta_Cliente");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("Movimiento");

            entity.HasIndex(e => e.MovimientoId, "IX_Movimiento_cuenta_id");

            entity.Property(e => e.MovimientoId)
                //.ValueGeneratedNever()
                .HasColumnName("movimiento_id");
            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("saldo_inicial");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_movimiento");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("valor");
            entity.Property(e => e.FechaMovimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.SaldoFinal)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("saldo_final");

            //entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
            //    .HasForeignKey(d => d.CuentaId)
            //    .HasConstraintName("FK_Movimiento_Cuenta");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.ToTable("Persona");

            entity.HasIndex(e => e.PersonaId, "IX_Persona_num_identificacion_index")
                .IsUnique();

            entity.Property(e => e.PersonaId).HasColumnName("persona_id");

            entity.Property(e => e.Edad).HasColumnName("edad");

            entity.Property(e => e.Genero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genero");

            entity.Property(e => e.Identificacion)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("identificacion");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");
            
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
