using System;

namespace WebAcademias.Data;

public class Noticia
{
    public long Id { get; set; }

    public string? Titulo { get; set; }

    public DateTime Fecha { get; set; }

    public string? Subtitulo { get; set; }

    public string? Cuerpo { get; set; }

    public string? Asociacion { get; set; }

    public string? ImagenRuta { get; set; }

    public string? ImagenNombre { get; set; }
}