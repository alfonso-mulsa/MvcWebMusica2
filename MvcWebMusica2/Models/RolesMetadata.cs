using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(RolesMetadata))]
    public partial class Roles { }
    public class RolesMetadata
    {
        public int Id { get; set; }

        public string? Descripcion { get; set; }
    }
}
