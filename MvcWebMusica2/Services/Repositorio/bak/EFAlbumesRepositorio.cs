using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio.bak
{
    public class EFAlbumesRepositorio : IAlbumesRepositorio
    {
        private readonly GrupoBContext _context = new();
        public bool Agregar(Albumes album)
        {
            if (DameUno(album.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Albumes.Add(album);
                _context.SaveChanges();
                return true;
            }
        }

        public bool Borrar(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Albumes.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Albumes> DameTodos()
        {
            return _context.Albumes.AsNoTracking().ToList();
        }

        public Albumes? DameUno(int Id)
        {
            return _context.Albumes.Find(Id);
        }

        public void Modificar(int Id, Albumes album)
        {
            _context.Albumes.Update(album);
            _context.SaveChanges();
        }
    }
}
