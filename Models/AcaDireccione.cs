using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaDireccione
{
    public int DirId { get; set; }

    public int? DirAcaId { get; set; }

    public string? DirCalle { get; set; }

    public string? DirNumero { get; set; }

    public string? DirCiudad { get; set; }

    public string? DirProvincia { get; set; }

    public string? DirCodigoPostal { get; set; }

    public decimal? DirLongitud { get; set; }

    public decimal? DirLatitud { get; set; }

    public virtual AcaAcademia? DirAca { get; set; }
}
