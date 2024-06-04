using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public class ListaCancionesAlbumes : IListableCancionesAlbumes
    {
        private readonly GrupoBContext _context;

        public ListaCancionesAlbumes(GrupoBContext contexto)
        {
            _context = contexto;
        }

        public List<Canciones> dameCanciones(int albumId)
        {
            return _context.Canciones.Where(item => item.AlbumesId == albumId).ToList();
        }
    }
}
