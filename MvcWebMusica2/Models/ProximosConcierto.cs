using System;
using System.Collections.Generic;

namespace MvcWebMusica2.Models;

public  class ProximosConcierto
{
    public int Id { get; set; }

    public DateOnly? Fecha { get; set; }

    public string? Gira { get; set; }

    public string? Ciudad { get; set; }

    public string? Grupo { get; set; }
}
