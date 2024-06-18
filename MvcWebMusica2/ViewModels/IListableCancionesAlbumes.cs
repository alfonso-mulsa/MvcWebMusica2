using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public interface IListableCancionesAlbumes
    {
        List<Canciones> DameCanciones(int albumId);
    }
}
