using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class GesNoticia
{
    public long NotId { get; set; }

    public DateTime NotFecha { get; set; }

    public string NotTitulo { get; set; } = null!;

    public string? NotTituloEn { get; set; }

    public string? NotTituloCa { get; set; }

    public string? NotSubtitulo { get; set; }

    public string? NotSubtituloEn { get; set; }

    public string? NotSubtituloCa { get; set; }

    public string? NotResumen { get; set; }

    public string? NotResumenEn { get; set; }

    public string? NotResumenCa { get; set; }

    public string? NotCuerpo { get; set; }

    public string? NotCuerpoEn { get; set; }

    public string? NotCuerpoCa { get; set; }

    public long? NotImagenPortada { get; set; }

    public string? NotUrlAlias { get; set; }

    public bool NotPublicada { get; set; }

    public bool NotPortada { get; set; }

    public bool NotPublicarPim { get; set; }

    public bool NotPublicarCar { get; set; }

    public bool NotPublicarBar { get; set; }

    public bool NotPublicarAsc { get; set; }

    public int NotVisitas { get; set; }

    public long? NotUsuario { get; set; }

    public long? DrupalNid { get; set; }

    public long? DrupalUid { get; set; }

    public DateTime? NotFechaPublicacion { get; set; }

    public virtual GesImagenes? NotImagenPortadaNavigation { get; set; }

    public virtual ICollection<GesNoticiaImagen> GesNoticiaImagenes { get; set; } = new List<GesNoticiaImagen>();

    public virtual ICollection<GesNoticiaAsociacion> GesNoticiaAsociaciones { get; set; } = new List<GesNoticiaAsociacion>();
}
