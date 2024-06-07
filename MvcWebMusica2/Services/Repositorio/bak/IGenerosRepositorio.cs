using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio.bak
{
    public interface IGenerosRepositorio
    {
        List<Generos> DameTodos();
        Generos? DameUno(int Id);
        bool Borrar(int Id);
        bool Agregar(Generos genero);
        void Modificar(int Id, Generos genero);
    }
}
