﻿using System.Data;
using System.Linq.Expressions;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class AlbumesController(
        IGenericRepositorio<Albumes> repositorioAlbumes,
        IGenericRepositorio<Grupos> repositorioGrupos,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Canciones> repositorioCanciones)
        : Controller
    {
        private readonly string _nombre = "Nombre";
        private readonly string _generosId = "GenerosId";
        private readonly string _gruposId = "GruposId";

        private async Task<IActionResult> VisualizaListaAlbumes(string vista)
        {
            var listaAlbumes = await repositorioAlbumes.DameTodos();
            foreach (var album in listaAlbumes)
            {
                album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
                album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
                album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            }
            return View(vista, listaAlbumes);
        }

        private async Task<IActionResult> VisualizaAlbum(int? id, string vista)
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

            if (vista == "Edit")
            {
                ViewData[_generosId] = new SelectList(await repositorioGeneros.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
                ViewData[_gruposId] = new SelectList(await repositorioGrupos.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            }
            else
            {
                album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
                album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
                album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            }

            return View(vista, album);
        }

        // GET: Albumes
        public async Task<IActionResult> Index()
        //public void Index()
        {
            return await VisualizaListaAlbumes("Index");
            //var listaAlbumes = await repositorioAlbumes.DameTodos();
            //foreach (var album in listaAlbumes)
            //{
            //    album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
            //    album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
            //    album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
            //}
            //return View(listaAlbumes);
        }

        // GET: Albumes y Canciones
        public async Task<IActionResult> AlbumesYCanciones()
        //public void AlbumesYCanciones()
        {
            return await VisualizaListaAlbumes("AlbumesYCanciones");
            //var listaAlbumes = await repositorioAlbumes.DameTodos();

            //foreach (var album in listaAlbumes)
            //{
            //    album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
            //    album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
            //}

            //return View(listaAlbumes);
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        //public void Details(int? id)
        {
            return await VisualizaAlbum(id, "Details");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var album = await repositorioAlbumes.DameUno(id);

            //if (album == null)
            //{
            //    return NotFound();
            //}

            //album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
            //album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
            //album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);

            //return View(album);
        }

        // GET: Albumes/Create
        public async Task<IActionResult> Create()
        {
            ViewData[_generosId] = new SelectList(await repositorioGeneros.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            ViewData[_gruposId] = new SelectList(await repositorioGrupos.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
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

            ViewData[_generosId] = new SelectList(await repositorioGeneros.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            ViewData[_gruposId] = new SelectList(await repositorioGrupos.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            return View(album);
        }

        // GET: Albumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return await VisualizaAlbum(id, "Edit");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var album = await repositorioAlbumes.DameUno(id);
            //if (album == null)
            //{
            //    return NotFound();
            //}

            //ViewData[_generosId] = new SelectList(await repositorioGeneros.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            //ViewData[_gruposId] = new SelectList(await repositorioGrupos.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            //return View(album);
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
                    if (!await AlbumesExists(album.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[_generosId] = new SelectList(await repositorioGeneros.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            ViewData[_gruposId] = new SelectList(await repositorioGrupos.DameTodosOrdenados(x => x.Nombre!), "Id", _nombre);
            return View(album);
        }

        // GET: Albumes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await VisualizaAlbum(id, "Delete");
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var album = await repositorioAlbumes.DameUno(id);

            //if (album == null)
            //{
            //    return NotFound();
            //}

            //album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
            //album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
            //album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);

            //return View(album);
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

        private async Task<bool> AlbumesExists(int id)
        {
            var lista = await repositorioAlbumes.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var albumes = await repositorioAlbumes.DameTodos();
            foreach (var album in albumes)
            {
                album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
                album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
            }
            var nombreArchivo = "Albumes.xlsx";
            return GenerarExcel(nombreArchivo, albumes);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Albumes> albumes)
        {
            DataTable dataTable = new("Albumes");
            dataTable.Columns.AddRange([
                new("Nombre"),
                new("Fecha"),
                new("Generos"),
                new("Grupos")
            ]);

            foreach (var album in albumes)
            {
                dataTable.Rows.Add(
                    album.Nombre,
                    album.Fecha,
                    album.Generos?.Nombre,
                    album.Grupos?.Nombre);
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dataTable);

            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                nombreArchivo);
        }
    }
}
