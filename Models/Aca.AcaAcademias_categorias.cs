using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaAcademias_categorias
{
    public int Cat_Id { get; set; }

    public string? Aca_id { get; set; }

    public virtual AcaAcademia? Academia { get; set; }
    public virtual AcaCategoria? Categoria { get; set; }
}