using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using System.Text.RegularExpressions;

namespace MvcWebMusica2.Services.Repositorio
{
    public class EFGenerosRepositorio : IGenerosRepositorio
    {
        private readonly GrupoBContext _context = new();
        public bool Agregar(Generos genero)
        {
            if (DameUno(genero.Id) != null)
            {
                return false;
            }
            else
            {
                _context.Generos.Add(genero);
                _context.SaveChanges();
                return true;
            }
        }

        public bool Borrar(int Id)
        {
            if (DameUno(Id) != null)
            {
                _context.Generos.Remove(DameUno(Id));
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Generos> DameTodos()
        {
            return _context.Generos.AsNoTracking().ToList();
        }

        public Generos? DameUno(int Id)
        {
            return _context.Generos.Find(Id);
        }

        public void Modificar(int Id, Generos genero)
        {
            _context.Generos.Update(genero);
            _context.SaveChanges();
        }
    }
}
