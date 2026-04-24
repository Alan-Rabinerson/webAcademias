using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaImagene
{
    public long ImgId { get; set; }

    public string? ImgPath { get; set; }

    public DateTime? ImgFecha { get; set; }

    public string? ImgNombre { get; set; }
}
