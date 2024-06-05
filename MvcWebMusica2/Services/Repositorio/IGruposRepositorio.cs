using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio
{
    public interface IGruposRepositorio
    {
        List<Grupos> DameTodos();
        Grupos? DameUno(int Id);
        bool Borrar(int Id);
        bool Agregar(Grupos grupo);
        void Modificar(int Id, Grupos grupo);
    }
}
