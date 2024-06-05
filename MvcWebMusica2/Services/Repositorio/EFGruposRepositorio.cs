using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio
{
    public class EFGruposRepositorio : IGruposRepositorio
    {
        private readonly GrupoBContext _context = new();
        public bool Agregar(Grupos grupo)
        {
            if (DameUno(grupo.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Grupos.Add(grupo);
                _context.SaveChanges();
                return true;
            }
        }

        public bool Borrar(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Grupos.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Grupos> DameTodos()
        {
            return _context.Grupos.AsNoTracking().ToList();
        }

        public Grupos? DameUno(int Id)
        {
            return _context.Grupos.Find(Id);
        }

        public void Modificar(int Id, Grupos grupo)
        {
            _context.Grupos.Update(grupo);
            _context.SaveChanges();
        }
    }
}
