using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public interface IListableCancionesAlbumes
    {
        List<Canciones> dameCanciones(int albumId);
    }
}
