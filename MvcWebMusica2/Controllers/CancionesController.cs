using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class CancionesController(
        IGenericRepositorio<Canciones> repositorioCanciones,
        IGenericRepositorio<Albumes> repositorioAlbumes
        )
        : Controller
    {
        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            var listaCanciones = await repositorioCanciones.DameTodos();
            foreach (var cancion in listaCanciones)
            {
                cancion.Albumes = await repositorioAlbumes.DameUno(cancion.AlbumesId);
            }
            return View(listaCanciones);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            
            if (cancion == null)
            {
                return NotFound();
            }
            else
            {
                cancion.Albumes = await repositorioAlbumes.DameUno(cancion.AlbumesId);
            }

            return View(cancion);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,AlbumesId,Single")] Canciones cancion)
        {
            if (ModelState.IsValid)
            {
                repositorioCanciones.Agregar(cancion);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracion,AlbumesId,Single")] Canciones cancion)
        {
            if (id != cancion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioCanciones.Modificar(id, cancion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CancionesExists(cancion.Id))
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
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion != null)
            {
                await repositorioCanciones.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CancionesExists(int id)
        {
            return repositorioCanciones.DameUno(id) != null;
        }
    }
}
