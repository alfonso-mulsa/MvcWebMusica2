using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(CiudadesMetadata))]
    public partial class Ciudades { }
    public class CiudadesMetadata
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public int? PaisesID { get; set; }

        public virtual Paises? Paises { get; set; }
    }
}
