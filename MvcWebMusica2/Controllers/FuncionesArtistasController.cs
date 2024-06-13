using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Controllers
{
    public class FuncionesArtistasController(GrupoBContext context) : Controller
    {
        // GET: FuncionesArtistas
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.FuncionesArtistas.Include(f => f.Artistas).Include(f => f.Funciones);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: FuncionesArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }

            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Create
        public IActionResult Create()
        {
            ViewData["ArtistasId"] = new SelectList(context.Artistas, "Id", "Nombre");
            ViewData["FuncionesId"] = new SelectList(context.Funciones, "Id", "Nombre");
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
                context.Add(funcionesArtistas);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
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
                    context.Update(funcionesArtistas);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionesArtistasExists(funcionesArtistas.Id))
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
            ViewData["ArtistasId"] = new SelectList(context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }

            return View(funcionesArtistas);
        }

        // POST: FuncionesArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionesArtistas = await context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtistas != null)
            {
                context.FuncionesArtistas.Remove(funcionesArtistas);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionesArtistasExists(int id)
        {
            return context.FuncionesArtistas.Any(e => e.Id == id);
        }

        //[HttpGet]
        //public async Task<FileResult> DescargarExcel()
        //{
        //    var funcionesArtistas = await repositorioFuncionesArtistas.DameTodos();
        //    var nombreArchivo = $"FuncionesArtistas.xlsx";
        //    return GenerarExcel(nombreArchivo, funcionesArtistas);
        //}

        //private FileResult GenerarExcel(string nombreArchivo, IEnumerable<FuncionesArtistas> funcionesArtistas)
        //{
        //    DataTable dataTable = new DataTable("FuncionesArtistas");
        //    dataTable.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("Artistas"),
        //        new DataColumn("Funciones")
        //    });

        //    foreach (var funcionesArtista in funcionesArtistas)
        //    {
        //        dataTable.Rows.Add(
        //            funcionesArtista.Artistas.Nombre,
        //            funcionesArtista.Funciones.Nombre);
        //    }

        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dataTable);

        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(),
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                nombreArchivo);
        //        }
        //    }
        //}
    }
}
