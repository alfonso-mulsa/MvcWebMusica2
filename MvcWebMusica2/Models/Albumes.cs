using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models;

public partial class Albumes
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Campo requerido.")]
    [Length(1, 50, ErrorMessage = "La longitud debe ser entre 1 y 50 caracteres.")]
    public string? Nombre { get; set; }
    
    [Required(ErrorMessage = "Campo requerido.")]
    [DisplayName("Género")]
    public int? GenerosId { get; set; }
    
    [Required(ErrorMessage = "Campo requerido.")]
    [DisplayName("Grupo")]
    public int? GruposId { get; set; }

    [Required(ErrorMessage = "Campo requerido.")]
    [DataType(DataType.Date)]
    public DateOnly? Fecha { get; set; }

    public virtual ICollection<Canciones> Canciones { get; set; } = new List<Canciones>();

    [DisplayName("Género")]
    public virtual Generos? Generos { get; set; }

    [DisplayName("Grupo")]
    public virtual Grupos? Grupos { get; set; }
}
