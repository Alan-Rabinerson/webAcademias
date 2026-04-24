using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAcademias.Models;

namespace WebAcademias.Data;

public partial class BloggingContext : DbContext
{
    public BloggingContext()
    {
    }

    public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcaAcademia> AcaAcademias { get; set; }

    public virtual DbSet<AcaCategoria> AcaCategorias { get; set; }

    public virtual DbSet<AcaDireccione> AcaDirecciones { get; set; }

    public virtual DbSet<AcaImagene> AcaImagenes { get; set; }

    public virtual DbSet<AcaTelefono> AcaTelefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Kept only as a fallback for design-time usage.
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_100_CI_AI");

        modelBuilder.Entity<AcaAcademia>(entity =>
        {
            entity.HasKey(e => e.AcaId);

            entity.ToTable("aca_academias");

            entity.Property(e => e.AcaId).HasColumnName("aca_id");
            entity.Property(e => e.AcaDescripcion)
                .IsUnicode(false)
                .HasColumnName("aca_descripcion");
            entity.Property(e => e.AcaFacebook)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_facebook");
            entity.Property(e => e.AcaInstagram)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_instagram");
            entity.Property(e => e.AcaLogo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_logo");
            entity.Property(e => e.AcaNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_nombre");
            entity.Property(e => e.AcaPoblacion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_poblacion");
            entity.Property(e => e.AcaServicios).HasColumnName("aca_servicios");
            entity.Property(e => e.AcaTwitter)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_twitter");
            entity.Property(e => e.AcaUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("aca_url");

            entity.HasMany(d => d.Cats).WithMany(p => p.Acas)
                .UsingEntity<Dictionary<string, object>>(
                    "AcaAcademiasCategoria",
                    r => r.HasOne<AcaCategoria>().WithMany()
                        .HasForeignKey("CatId")
                        .HasConstraintName("FK_aca_academias_categorias_aca_categorias"),
                    l => l.HasOne<AcaAcademia>().WithMany()
                        .HasForeignKey("AcaId")
                        .HasConstraintName("FK_aca_academias_categorias_aca_academias"),
                    j =>
                    {
                        j.HasKey("AcaId", "CatId");
                        j.ToTable("aca_academias_categorias");
                        j.IndexerProperty<int>("AcaId").HasColumnName("aca_id");
                        j.IndexerProperty<int>("CatId").HasColumnName("cat_id");
                    });
        });

        modelBuilder.Entity<AcaCategoria>(entity =>
        {
            entity.HasKey(e => e.CatId);

            entity.ToTable("aca_categorias");

            entity.Property(e => e.CatId).HasColumnName("cat_id");
            entity.Property(e => e.CatDescripcion)
                .HasMaxLength(255)
                .HasColumnName("cat_descripcion");
            entity.Property(e => e.CatMateria)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cat_materia");
            entity.Property(e => e.CatNombre)
                .HasMaxLength(255)
                .HasColumnName("cat_nombre");
        });

        modelBuilder.Entity<AcaDireccione>(entity =>
        {
            entity.HasKey(e => e.DirId);

            entity.ToTable("aca_direcciones");

            entity.Property(e => e.DirId).HasColumnName("dir_id");
            entity.Property(e => e.DirAcaId).HasColumnName("dir_aca_id");
            entity.Property(e => e.DirCalle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dir_calle");
            entity.Property(e => e.DirCiudad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dir_ciudad");
            entity.Property(e => e.DirCodigoPostal)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dir_codigo_postal");
            entity.Property(e => e.DirLatitud)
                .HasColumnType("decimal(10, 6)")
                .HasColumnName("dir_latitud");
            entity.Property(e => e.DirLongitud)
                .HasColumnType("decimal(10, 6)")
                .HasColumnName("dir_longitud");
            entity.Property(e => e.DirNumero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dir_numero");
            entity.Property(e => e.DirProvincia)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dir_provincia");

            entity.HasOne(d => d.DirAca).WithMany(p => p.AcaDirecciones)
                .HasForeignKey(d => d.DirAcaId)
                .HasConstraintName("FK__aca_direc__dir_a__06EE9736");
        });

        modelBuilder.Entity<AcaImagene>(entity =>
        {
            entity.HasKey(e => e.ImgId).HasName("PK__aca_imag__6F16A71CFA9BB045");

            entity.ToTable("aca_imagenes");

            entity.Property(e => e.ImgId).HasColumnName("img_id");
            entity.Property(e => e.ImgFecha)
                .HasColumnType("datetime")
                .HasColumnName("img_fecha");
            entity.Property(e => e.ImgNombre)
                .HasMaxLength(150)
                .HasColumnName("img_nombre");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(150)
                .HasColumnName("img_path");
        });

        modelBuilder.Entity<AcaTelefono>(entity =>
        {
            entity.HasKey(e => e.TelId).HasName("PK_aca_telefono");

            entity.ToTable("aca_telefonos");

            entity.Property(e => e.TelId).HasColumnName("tel_id");
            entity.Property(e => e.TelAcaId).HasColumnName("tel_aca_id");
            entity.Property(e => e.TelNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tel_nombre");
            entity.Property(e => e.TelNumero)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tel_numero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
