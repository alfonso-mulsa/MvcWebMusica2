using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class FuncionesArtistasController(
        IGenericRepositorio<FuncionesArtistas> repositorioFuncionesArtistas,
        IGenericRepositorio<Artistas> repositorioArtistas,
        IGenericRepositorio<Funciones> repositorioFunciones
        ) : Controller
    {
        private readonly string nombre = "Nombre";
        private readonly string artistasId = "ArtistasId";
        private readonly string funcionesId = "FuncionesId";
        // GET: FuncionesArtistas
        public async Task<IActionResult> Index()
        {
            var listaFuncionesArtistas = await repositorioFuncionesArtistas.DameTodos();
            foreach (var funcionArtista in listaFuncionesArtistas)
            {
                funcionArtista.Artistas = await repositorioArtistas.DameUno(funcionArtista.ArtistasId);
                funcionArtista.Funciones = await repositorioFunciones.DameUno(funcionArtista.FuncionesId);
            }
            return View(listaFuncionesArtistas);
        }

        // GET: FuncionesArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await repositorioFuncionesArtistas.DameUno(id);

            if (funcionesArtistas == null)
            {
                return NotFound();
            }
            else
            {
                funcionesArtistas.Artistas = await repositorioArtistas.DameUno(funcionesArtistas.ArtistasId);
                funcionesArtistas.Funciones = await repositorioFunciones.DameUno(funcionesArtistas.FuncionesId);
            }

            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[artistasId] = new SelectList(await repositorioArtistas.DameTodos(), "Id", nombre);
            ViewData[funcionesId] = new SelectList(await repositorioFunciones.DameTodos(), "Id", nombre);
            return View();
        }

        // POST: FuncionesArtistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuncionesId,ArtistasId")] FuncionesArtistas funcionesArtistas)
        {
            if (ModelState.IsValid)
            {
                await repositorioFuncionesArtistas.Agregar(funcionesArtistas);
                return RedirectToAction(nameof(Index));
            }
            ViewData[artistasId] = new SelectList(await repositorioArtistas.DameTodos(), "Id", nombre, funcionesArtistas.ArtistasId);
            ViewData[funcionesId] = new SelectList(await repositorioFunciones.DameTodos(), "Id", nombre, funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await repositorioFuncionesArtistas.DameUno(id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }
            ViewData[artistasId] = new SelectList(await repositorioArtistas.DameTodos(), "Id", nombre, funcionesArtistas.ArtistasId);
            ViewData[funcionesId] = new SelectList(await repositorioFunciones.DameTodos(), "Id", nombre, funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // POST: FuncionesArtistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuncionesId,ArtistasId")] FuncionesArtistas funcionesArtistas)
        {
            if (id != funcionesArtistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioFuncionesArtistas.Modificar(id, funcionesArtistas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FuncionesArtistasExists(funcionesArtistas.Id))
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
            ViewData[artistasId] = new SelectList(await repositorioArtistas.DameTodos(), "Id", nombre, funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(await repositorioFunciones.DameTodos(), "Id", nombre, funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await repositorioFuncionesArtistas.DameUno(id);

            if (funcionesArtistas == null)
            {
                return NotFound();
            }
            else
            {
                funcionesArtistas.Artistas = await repositorioArtistas.DameUno(funcionesArtistas.ArtistasId);
                funcionesArtistas.Funciones = await repositorioFunciones.DameUno(funcionesArtistas.FuncionesId);
            }

            return View(funcionesArtistas);
        }

        // POST: FuncionesArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionesArtistas = await repositorioFuncionesArtistas.DameUno(id);
            if (funcionesArtistas != null)
            {
                await repositorioFuncionesArtistas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FuncionesArtistasExists(int id)
        {
            var elemento = await repositorioFuncionesArtistas.DameTodos();
            return elemento.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var funcionesArtistas = await repositorioFuncionesArtistas.DameTodos();
            foreach (var funcionArtista in funcionesArtistas)
            {
                funcionArtista.Artistas = await repositorioArtistas.DameUno(funcionArtista.ArtistasId);
                funcionArtista.Funciones = await repositorioFunciones.DameUno(funcionArtista.FuncionesId);
            }
            var nombreArchivo = "FuncionesArtistas.xlsx";
            return GenerarExcel(nombreArchivo, funcionesArtistas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<FuncionesArtistas> funcionesArtistas)
        {
            DataTable dataTable = new("FuncionesArtistas");
            dataTable.Columns.AddRange([
                new("Artistas"),
                new("Funciones")
            ]);

            foreach (var funcionesArtista in funcionesArtistas)
            {
                dataTable.Rows.Add(
                    funcionesArtista.Artistas?.Nombre,
                    funcionesArtista.Funciones?.Nombre);
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
