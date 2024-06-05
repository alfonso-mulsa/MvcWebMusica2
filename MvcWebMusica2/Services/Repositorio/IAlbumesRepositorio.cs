using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio
{
    public interface IAlbumesRepositorio
    {
        List<Albumes> DameTodos();
        Albumes? DameUno(int Id);
        bool Borrar(int Id);
        bool Agregar(Albumes album);
        void Modificar(int Id, Albumes album);
    }
}
