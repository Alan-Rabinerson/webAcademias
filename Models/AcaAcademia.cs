using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaAcademia
{
    public int AcaId { get; set; }

    public string AcaNombre { get; set; } = null!;

    public string? AcaDescripcion { get; set; }

    public string? AcaUrl { get; set; }

    public string? AcaFacebook { get; set; }

    public string? AcaInstagram { get; set; }

    public string? AcaPoblacion { get; set; }

    public string? AcaTwitter { get; set; }

    public string? AcaLogo { get; set; }

    public string? AcaServicios { get; set; }

    public virtual ICollection<AcaDireccione> AcaDirecciones { get; set; } = new List<AcaDireccione>();

    public virtual ICollection<AcaCategoria> Cats { get; set; } = new List<AcaCategoria>();
}
