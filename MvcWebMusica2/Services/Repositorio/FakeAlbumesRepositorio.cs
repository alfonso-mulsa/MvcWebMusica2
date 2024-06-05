using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Repositorio
{
    public class FakeAlbumesRepositorio : IAlbumesRepositorio
    {
        private List<Albumes> listaAlbumes = new();

        public FakeAlbumesRepositorio()
        {
            Albumes nuevoAlbum = new()
            {
                Id = 1,
                Nombre = "Queen",
                Fecha = DateOnly.ParseExact("19730713", "yyyyMMdd", CultureInfo.CurrentCulture),
                Canciones = null,
                Generos = null,
                GenerosId = 1,
                GruposId = 2,
                Grupos = null
            };
            listaAlbumes.Add(nuevoAlbum);
            nuevoAlbum = new()
            {
                Id = 2,
                Nombre = "The Number of the Beast",
                Fecha = DateOnly.ParseExact("19820101", "yyyyMMdd", CultureInfo.CurrentCulture),
                Canciones = null,
                Generos = null,
                GenerosId = 6,
                GruposId = 13,
                Grupos = null
            };
            listaAlbumes.Add(nuevoAlbum);
            nuevoAlbum = new()
            {
                Id = 3,
                Nombre = "Who Are You",
                Fecha = DateOnly.ParseExact("19780821", "yyyyMMdd", CultureInfo.CurrentCulture),
                Canciones = null,
                Generos = null,
                GenerosId = 1,
                GruposId = 14,
                Grupos = null
            };
            listaAlbumes.Add(nuevoAlbum);
            nuevoAlbum = new()
            {
                Id = 4,
                Nombre = "Static Age",
                Fecha = DateOnly.ParseExact("19960227", "yyyyMMdd", CultureInfo.CurrentCulture),
                Canciones = null,
                Generos = null,
                GenerosId = 7,
                GruposId = 15,
                Grupos = null
            };
            listaAlbumes.Add(nuevoAlbum);
        }

        public bool Agregar(Albumes album)
        {
            if (DameUno(album.Id) != null)
            {
                return false;
            }
            else
            {
                int ultimoId = listaAlbumes.Max(x => x.Id);
                if (album.Id == 0)
                {
                    album.Id = ultimoId + 1;
                }
                listaAlbumes.Add(album);
                return true;
            }
        }

        public bool Borrar(int Id)
        {
            if (DameUno(Id) != null)
            {
                listaAlbumes.Remove(DameUno(Id));
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Albumes> DameTodos()
        {
            return listaAlbumes;
            ;
        }

        public Albumes? DameUno(int Id)
        {
            return listaAlbumes.FirstOrDefault(x => x.Id == Id);
        }

        public void Modificar(int Id, Albumes album)
        {
            var albumAModificar = DameUno(Id);
            if (albumAModificar != null)
            {
                Borrar(Id);
            }
            Agregar(album);
        }
    }
}
