using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class CancionSpecification(int AlbumId) : ICancionSpecification
    {
        public bool IsValid(Canciones cancion)
        {
            return cancion.AlbumesId == AlbumId;
        }
    }
}
