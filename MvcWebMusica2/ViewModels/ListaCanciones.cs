using MvcWebMusica2.Models;

namespace MvcWebMusica2.ViewModels
{
    public class ListaCanciones : IListableCanciones
    {
        private readonly GrupoBContext _context;

        public ListaCanciones(GrupoBContext contexto)
        {
            _context = contexto;
        }

        public List<Canciones> dameCanciones(int albumId)
        {
            return _context.Canciones.Where(item => item.AlbumesId == albumId).ToList();
        }
    }
}
