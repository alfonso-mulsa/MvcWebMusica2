using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(EmpleadosMetadata))]
    public partial class Empleados { }
    public class EmpleadosMetadata
    {
        public int Id { get; set; }

        public string? NombreCompleto { get; set; }

        public int? RolesId { get; set; }

        public virtual Roles? Roles { get; set; }
    }
}
