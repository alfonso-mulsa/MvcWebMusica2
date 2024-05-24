using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MvcWebMusica2.Models
{
    [ModelMetadataType(typeof(VideoClipsMetadata))]
    public partial class VideoClips { }
    public class VideoClipsMetadata
    {
        public int Id { get; set; }

        public int? CancionesId { get; set; }

        public DateOnly? Fecha { get; set; }

        public virtual Canciones? Canciones { get; set; }
    }
}
