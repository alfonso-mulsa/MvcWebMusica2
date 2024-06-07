using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using System.Linq.Expressions;

namespace MvcWebMusica2.Services.Repositorio
{
    public class EFGenericRepositorio<T> : IGenericRepositorio<T> where T : class
    {
        private readonly GrupoBContext _context = new();
        public bool Agregar(T element)
        {
            _context.Set<T>().Add(element);
            _context.SaveChanges();
            return true;
        }

        public bool Borrar(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Set<T>().Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<T> DameTodos()
        {
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T? DameUno(int? Id)
        {
            if (Id == null)
            {
                return null;
            }
            return _context.Set<T>().Find(Id);
        }

        public List<T> Filtra(Expression<Func<T, bool>> predicado)
        {
            return _context.Set<T>().Where<T>(predicado).ToList();
        }

        public void Modificar(int Id, T element)
        {
            _context.Set<T>().Update(element);
            _context.SaveChanges();
        }
    }
}
