using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class ArtistasController(
        IGenericRepositorio<Artistas> repositorioArtistas,
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Grupos> repositorioGrupos)
        : Controller
    {
        //private readonly GrupoBContext _context;

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            var listaArtistas = await repositorioArtistas.DameTodos();
            foreach (var artista in listaArtistas)
            {
                artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
                artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
                artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);
            }
            return View(listaArtistas);
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);

            return View(artista);
        }

        // GET: Artistas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (ModelState.IsValid)
            {
                await repositorioArtistas.Agregar(artista);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            
            if (artista == null)
            {
                return NotFound();
            }

            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioArtistas.Modificar(id, artista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistasExists(artista.Id))
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
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await repositorioArtistas.DameUno(id);
            if (artista != null)
            {
                await repositorioArtistas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ArtistasExists(int id)
        {
            return repositorioArtistas.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var artistas = await repositorioArtistas.DameTodos();
            foreach (var artista in artistas)
            {
                artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
                artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
                artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);
            }
            var nombreArchivo = $"Artistas.xlsx";
            return GenerarExcel(nombreArchivo, artistas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Artistas> artistas)
        {
            DataTable dataTable = new DataTable("Artistas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Nombre"),
                new("FechaDeNacimiento"),
                new("Ciudades"),
                new("Generos"),
                new("Grupos")
            });

            foreach (var artista in artistas)
            {
                dataTable.Rows.Add(
                    artista.Nombre,
                    artista.FechaDeNacimiento,
                    artista.Ciudades.Nombre,
                    artista.Generos.Nombre,
                    artista.Grupos?.Nombre
                    );
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
