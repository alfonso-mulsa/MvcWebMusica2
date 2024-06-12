using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.ViewModels;

namespace MvcWebMusica2.Controllers
{
    public class AlbumesController(
        IGenericRepositorio<Albumes> repositorioAlbumes,
        IGenericRepositorio<Grupos> repositorioGrupos,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Canciones> repositorioCanciones)
        : Controller
    {
        //private readonly GrupoBContext _context;
        //private readonly IListableCancionesAlbumes _listadorCancionesAlbumes;

        //_context = context;

        // GET: Albumes
        public async Task<IActionResult> Index()
        {
            var listaAlbumes = await repositorioAlbumes.DameTodos();
            foreach (var album in listaAlbumes)
            {
                album.Generos = await repositorioGeneros.DameUno(album.GenerosId);
                album.Grupos = await repositorioGrupos.DameUno(album.GruposId);
                album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            }
            return View(listaAlbumes);
        }

        // GET: Albumes y Canciones
        public async Task<IActionResult> AlbumesYCanciones()
        {
            var listaAlbumes = await repositorioAlbumes.DameTodos();
            //var listaAlbumes = new List<Albumes>();
            //var album2 = await repositorioAlbumes.DameUno(1);
            //listaAlbumes.Add(album2);

            foreach (var album in listaAlbumes)
            {
                album.Generos = await repositorioGeneros.DameUno(album.GenerosId);
                album.Grupos = await repositorioGrupos.DameUno(album.GruposId);
                //album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
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

            var album = await repositorioAlbumes.DameUno(id);

            if (album == null)
            {
                return NotFound();
            }
            else
            {
                album.Generos = await repositorioGeneros.DameUno(album.GenerosId);
                album.Grupos = await repositorioGrupos.DameUno(album.GruposId);
                album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            }

            return View(album);
        }

        // GET: Albumes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre");
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
                await repositorioAlbumes.Agregar(album);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
            return View(album);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await repositorioAlbumes.DameUno(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
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
                    await repositorioAlbumes.Modificar(id, album);
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
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", album.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", album.GruposId);
            return View(album);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await repositorioAlbumes.DameUno(id);

            if (album == null)
            {
                return NotFound();
            }
            else
            {
                album.Generos = await repositorioGeneros.DameUno(album.GenerosId);
                album.Grupos = await repositorioGrupos.DameUno(album.GruposId);
                album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            }

            return View(album);
        }

        // POST: Albumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await repositorioAlbumes.DameUno(id);
            if (album != null)
            {
                await repositorioAlbumes.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AlbumesExists(int id)
        {
            return repositorioAlbumes.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var albumes = await repositorioAlbumes.DameTodos();
            var nombreArchivo = $"Albumes.xlsx";
            return GenerarExcel(nombreArchivo, albumes);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Albumes> albumes)
        {
            DataTable dataTable = new DataTable("Albumes");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nombre"),
                new DataColumn("Fecha"),
                new DataColumn("Generos"),
                new DataColumn("Grupos")
            });

            foreach (var album in albumes)
            {
                dataTable.Rows.Add(
                    album.Nombre,
                    album.Fecha,
                    album.Generos.Nombre,
                    album.Grupos.Nombre);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }
    }
}
