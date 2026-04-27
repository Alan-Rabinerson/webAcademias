namespace WebAcademias.Models;

public partial class GesNoticiaImagen
{
    public long NoiNoticia { get; set; }

    public long NoiImagen { get; set; }

    public string? NoiTitulo { get; set; }

    public int NoiOrden { get; set; }

    public virtual GesNoticia NoiNoticiaNavigation { get; set; } = null!;
}
