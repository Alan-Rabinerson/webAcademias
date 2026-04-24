using System;
using System.Collections.Generic;

namespace WebAcademias.Models;

public partial class AcaTelefono
{
    public int TelId { get; set; }

    public int TelAcaId { get; set; }

    public string TelNumero { get; set; } = null!;

    public string? TelNombre { get; set; }
}
