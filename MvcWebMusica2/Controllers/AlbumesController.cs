using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.ViewModels;

namespace MvcWebMusica2.Controllers
{
    public class AlbumesController : Controller
    {
        //private readonly GrupoBContext _context;
        private readonly IAlbumesRepositorio _repositorioAlbumes;
        private readonly IGruposRepositorio _repositorioGrupos;
        private readonly IGenerosRepositorio _repositorioGeneros;
        private readonly IListableCancionesAlbumes _listadorCancionesAlbumes;

        public AlbumesController(IAlbumesRepositorio repositorioAlbumes, IGruposRepositorio repositorioGrupos, IGenerosRepositorio repositorioGeneros, IListableCancionesAlbumes listadorCancionesAlbumes)
        {
            //_context = context;
            _repositorioAlbumes = repositorioAlbumes;
            _repositorioGrupos = repositorioGrupos;
            _repositorioGeneros = repositorioGeneros;
            _listadorCancionesAlbumes = listadorCancionesAlbumes;
        }

        // GET: Albumes
        public async Task<IActionResult> Index()
        {
            var listaAlbumes= _repositorioAlbumes.DameTodos();
            foreach (var item in listaAlbumes)
            {
                item.Generos = _repositorioGeneros.DameUno((int)item.GenerosId);
                item.Grupos = _repositorioGrupos.DameUno((int)item.GruposId);
                item.Canciones = _listadorCancionesAlbumes.dameCanciones(item.Id);
            }
            return View(listaAlbumes);
        }

        // GET: Albumes y Canciones
        public async Task<IActionResult> AlbumesYCanciones()
        {
            var listaAlbumes = _repositorioAlbumes.DameTodos();

            foreach (var item in listaAlbumes)
            {
                item.Generos = _repositorioGeneros.DameUno((int)item.GenerosId);
                item.Grupos = _repositorioGrupos.DameUno((int)item.GruposId);
                item.Canciones = _listadorCancionesAlbumes.dameCanciones(item.Id);
            }

            return View(listaAlbumes);
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _repositorioAlbumes.DameUno((int)id);
            album.Generos = _repositorioGeneros.DameUno((int)album.GenerosId);
            album.Grupos = _repositorioGrupos.DameUno((int)album.GruposId);
            album.Canciones = _listadorCancionesAlbumes.dameCanciones(album.Id);

            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // GET: Albumes/Create
        public IActionResult Create()
        {
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Albumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,GruposId,Fecha")] Albumes album)
        {
            if (ModelState.IsValid)
            {
                _repositorioAlbumes.Agregar(album);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
            return View(album);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _repositorioAlbumes.DameUno((int)id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
            return View(album);
        }

        // POST: Albumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,GruposId,Fecha")] Albumes album)
        {
            if (id != album.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositorioAlbumes.Modificar(id, album);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlbumesExists(album.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
            return View(album);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = _repositorioAlbumes.DameUno((int)id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = _repositorioAlbumes.DameUno((int)id);
            if (album != null)
            {
                _repositorioAlbumes.Borrar((int)id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumesExists(int id)
        {
            return _repositorioAlbumes.DameUno((int)id) != null;
        }
    }
}
