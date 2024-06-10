using System.Linq.Expressions;

namespace MvcWebMusica2.Services.Repositorio
{
    public interface IGenericRepositorio<T> where T : class
    {
        Task<bool> Agregar(T element);
        Task<bool> Borrar(int Id);
        Task<List<T>> DameTodos();
        Task<T?> DameUno(int? Id);
        Task<List<T>> Filtra(Expression<Func<T, bool>> predicado);
        void Modificar(int Id, T element);
    }
}
