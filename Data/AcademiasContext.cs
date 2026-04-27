using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebAcademias.Models;

namespace WebAcademias.Data;

public partial class AcademiasContext : DbContext
{
    public AcademiasContext()
    {
    }

    public AcademiasContext(DbContextOptions<AcademiasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcaAcademia> AcaAcademias { get; set; }

    public virtual DbSet<AcaCategoria> AcaCategorias { get; set; }

    public virtual DbSet<AcaDireccione> AcaDirecciones { get; set; }

    public virtual DbSet<AcaImagene> AcaImagenes { get; set; }

    public virtual DbSet<AcaTelefono> AcaTelefonos { get; set; }

    public virtual DbSet<GesImagenes> GesImagenes { get; set; }

    public virtual DbSet<GesNoticia> GesNoticias { get; set; }

    public virtual DbSet<GesNoticiaAsociacion> GesNoticiaAsociaciones { get; set; }

    public virtual DbSet<GesNoticiaImagen> GesNoticiaImagenes { get; set; }

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

        modelBuilder.Entity<GesImagenes>(entity =>
        {
            entity.HasKey(e => e.ImgId);

            entity.ToTable("ges_imagenes");

            entity.Property(e => e.ImgId).HasColumnName("img_id");
            entity.Property(e => e.ImgPath)
                .HasMaxLength(150)
                .HasColumnName("img_path");
            entity.Property(e => e.ImgFecha)
                .HasColumnType("datetime")
                .HasColumnName("img_fecha");
            entity.Property(e => e.ImgNombre)
                .HasMaxLength(150)
                .HasColumnName("img_nombre");
        });

        modelBuilder.Entity<GesNoticia>(entity =>
        {
            entity.HasKey(e => e.NotId);

            entity.ToTable("ges_noticias");

            entity.Property(e => e.NotId).HasColumnName("not_id");
            entity.Property(e => e.NotFecha)
                .HasColumnType("datetime")
                .HasColumnName("not_fecha");
            entity.Property(e => e.NotTitulo)
                .HasMaxLength(250)
                .HasColumnName("not_titulo");
            entity.Property(e => e.NotTituloEn)
                .HasMaxLength(250)
                .HasColumnName("not_titulo_en");
            entity.Property(e => e.NotTituloCa)
                .HasMaxLength(250)
                .HasColumnName("not_titulo_ca");
            entity.Property(e => e.NotSubtitulo)
                .HasMaxLength(350)
                .IsFixedLength()
                .HasColumnName("not_subtitulo");
            entity.Property(e => e.NotSubtituloEn)
                .HasMaxLength(350)
                .IsFixedLength()
                .HasColumnName("not_subtitulo_en");
            entity.Property(e => e.NotSubtituloCa)
                .HasMaxLength(350)
                .IsFixedLength()
                .HasColumnName("not_subtitulo_ca");
            entity.Property(e => e.NotResumen).HasColumnName("not_resumen");
            entity.Property(e => e.NotResumenEn).HasColumnName("not_resumen_en");
            entity.Property(e => e.NotResumenCa).HasColumnName("not_resumen_ca");
            entity.Property(e => e.NotCuerpo).HasColumnName("not_cuerpo");
            entity.Property(e => e.NotCuerpoEn).HasColumnName("not_cuerpo_en");
            entity.Property(e => e.NotCuerpoCa).HasColumnName("not_cuerpo_ca");
            entity.Property(e => e.NotImagenPortada).HasColumnName("not_imagen_portada");
            entity.Property(e => e.NotUrlAlias)
                .HasMaxLength(300)
                .HasColumnName("not_url_alias");
            entity.Property(e => e.NotPublicada).HasColumnName("not_publicada");
            entity.Property(e => e.NotPortada).HasColumnName("not_portada");
            entity.Property(e => e.NotPublicarPim).HasColumnName("not_publicar_pim");
            entity.Property(e => e.NotPublicarCar).HasColumnName("not_publicar_car");
            entity.Property(e => e.NotPublicarBar).HasColumnName("not_publicar_bar");
            entity.Property(e => e.NotPublicarAsc).HasColumnName("not_publicar_asc");
            entity.Property(e => e.NotVisitas).HasColumnName("not_visitas");
            entity.Property(e => e.NotUsuario).HasColumnName("not_usuario");
            entity.Property(e => e.DrupalNid).HasColumnName("drupal_nid");
            entity.Property(e => e.DrupalUid).HasColumnName("drupal_uid");
            entity.Property(e => e.NotFechaPublicacion)
                .HasColumnType("smalldatetime")
                .HasColumnName("not_fecha_publicacion");

            entity.HasOne(d => d.NotImagenPortadaNavigation)
                .WithMany(p => p.GesNoticias)
                .HasForeignKey(d => d.NotImagenPortada)
                .HasConstraintName("FK_ges_noticias_ges_imagenes");
        });

        modelBuilder.Entity<GesNoticiaAsociacion>(entity =>
        {
            entity.HasKey(e => new { e.NoaNoticia, e.NoaAsociacion });

            entity.ToTable("ges_noticia_asociacion");

            entity.Property(e => e.NoaNoticia).HasColumnName("noa_noticia");
            entity.Property(e => e.NoaAsociacion)
                .HasMaxLength(3)
                .HasColumnName("noa_asociacion");

            entity.HasOne(d => d.NoaNoticiaNavigation)
                .WithMany(p => p.GesNoticiaAsociaciones)
                .HasForeignKey(d => d.NoaNoticia)
                .HasConstraintName("FK_ges_noticia_asociacion_ges_noticias");
        });

        modelBuilder.Entity<GesNoticiaImagen>(entity =>
        {
            entity.HasKey(e => new { e.NoiNoticia, e.NoiImagen });

            entity.ToTable("ges_noticia_imagen");

            entity.Property(e => e.NoiNoticia).HasColumnName("noi_noticia");
            entity.Property(e => e.NoiImagen).HasColumnName("noi_imagen");
            entity.Property(e => e.NoiTitulo)
                .HasMaxLength(150)
                .HasColumnName("noi_titulo");
            entity.Property(e => e.NoiOrden).HasColumnName("noi_orden");

            entity.HasOne(d => d.NoiNoticiaNavigation)
                .WithMany(p => p.GesNoticiaImagenes)
                .HasForeignKey(d => d.NoiNoticia)
                .HasConstraintName("FK_ges_noticia_imagen_ges_noticias");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
