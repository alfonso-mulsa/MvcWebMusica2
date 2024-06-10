using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public class ListaCancionesAlbumes(GrupoBContext contexto) : IListableCancionesAlbumes
    {
        public List<Canciones> dameCanciones(int albumId)
        {
            List<Canciones> list = [];
            foreach (var canciones in contexto.Canciones.Where(item => item.AlbumesId == albumId)) list.Add(canciones);
            return list;
        }
    }
}
