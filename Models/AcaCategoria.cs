using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaCategoria
{
    public int CatId { get; set; }

    public string CatNombre { get; set; } = null!;

    public string? CatDescripcion { get; set; }

    public string? CatMateria { get; set; }

    public virtual ICollection<AcaAcademia> Acas { get; set; } = new List<AcaAcademia>();
}
