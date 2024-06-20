using System.Data;
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
        private const bool ConCanciones = true;
        private const bool SinCanciones = false;

        /// <summary>
        /// Método que devuelve la lista de todos los albumes con el género, el grupo y, opcionalemente, las canciones del album.
        /// </summary>
        /// <param name="canciones">
        /// Tipo Booleano (const ConCanciones / const SinCanciones) para indicar si hay que añadir, o no, las canciones al album.
        /// </param>
        /// <returns>
        /// Lista de todos los albumes con con el género, el grupo y, opcionalemente, las canciones del album.
        /// </returns>
        private async Task<List<Albumes>> DameListaAlbumes(bool incluyeCanciones)
        {
            var listaAlbumes = await repositorioAlbumes.DameTodos();
            foreach (var album in listaAlbumes)
            {
                album.Generos = await repositorioGeneros.DameUno((int)album.GenerosId!);
                album.Grupos = await repositorioGrupos.DameUno((int)album.GruposId!);
                if (incluyeCanciones)
                {
                    album.Canciones = await repositorioCanciones.Filtra(x => x.AlbumesId == album.Id);
                }
            }
            return listaAlbumes;
        }

        /// <summary>
        /// Método que devuelve la vista de un album.
        /// </summary>
        /// <param name="vista">Nombre de la vista.</param>
        /// <param name="id">Id del album a mostrar.</param>
        /// <returns>Vista de album</returns>
        private async Task<IActionResult> VistaAlbum(string vista, int? id)
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
        {
            return View(await DameListaAlbumes(ConCanciones));
        }

        // GET: Albumes y Canciones
        public async Task<IActionResult> AlbumesYCanciones()
        {
            return View(await DameListaAlbumes(SinCanciones));
        }

        // GET: Albumes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return await VistaAlbum("Details", id);
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
            return await VistaAlbum("Edit", id);
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
            return await VistaAlbum("Delete", id);
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
            var nombreArchivo = "Albumes.xlsx";
            return GenerarExcel(nombreArchivo, await DameListaAlbumes(SinCanciones));
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
