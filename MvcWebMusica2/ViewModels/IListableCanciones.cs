using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public interface IListableCanciones
    {
        List<Canciones> dameCanciones(int albumId);
    }
}
