namespace WebAcademias.Models;

public partial class GesNoticiaAsociacion
{
    public long NoaNoticia { get; set; }

    public string NoaAsociacion { get; set; } = null!;

    public virtual GesNoticia NoaNoticiaNavigation { get; set; } = null!;
}
