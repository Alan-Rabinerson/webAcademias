using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class GesImagenes
{
    public long ImgId { get; set; }

    public string? ImgPath { get; set; }

    public DateTime ImgFecha { get; set; }

    public string? ImgNombre { get; set; }

    public virtual ICollection<GesNoticia> GesNoticias { get; set; } = new List<GesNoticia>();
}
