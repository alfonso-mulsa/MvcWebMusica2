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
    public class CiudadesController(GrupoBContext context) : Controller
    {
        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.Ciudades.Include(c => c.Paises);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await context.Ciudades
                .Include(c => c.Paises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return View(ciudades);
        }

        // GET: Ciudades/Create
        public IActionResult Create()
        {
            ViewData["PaisesID"] = new SelectList(context.Paises, "Id", "Nombre");
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PaisesID")] Ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                context.Add(ciudades);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaisesID"] = new SelectList(context.Paises, "Id", "Nombre", ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await context.Ciudades.FindAsync(id);
            if (ciudades == null)
            {
                return NotFound();
            }
            ViewData["PaisesID"] = new SelectList(context.Paises, "Id", "Nombre", ciudades.PaisesID);
            return View(ciudades);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PaisesID")] Ciudades ciudades)
        {
            if (id != ciudades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(ciudades);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CiudadesExists(ciudades.Id))
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
            ViewData["PaisesID"] = new SelectList(context.Paises, "Id", "Nombre", ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudades = await context.Ciudades
                .Include(c => c.Paises)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ciudades == null)
            {
                return NotFound();
            }

            return View(ciudades);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudades = await context.Ciudades.FindAsync(id);
            if (ciudades != null)
            {
                context.Ciudades.Remove(ciudades);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CiudadesExists(int id)
        {
            return context.Ciudades.Any(e => e.Id == id);
        }

        //[HttpGet]
        //public async Task<FileResult> DescargarExcel()
        //{
        //    var ciudades = await repostorioCiudades.DameTodos();
        //    var nombreArchivo = $"Ciudades.xlsx";
        //    return GenerarExcel(nombreArchivo, ciudades);
        //}

        //private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Ciudades> ciudades)
        //{
        //    DataTable dataTable = new DataTable("Ciudades");
        //    dataTable.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("Nombre"),
        //        new DataColumn("Paises"),
        //    });

        //    foreach (var ciudad in ciudades)
        //    {
        //        dataTable.Rows.Add(
        //            ciudad.Nombre,
        //            ciudad.Paises.Nombre);
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
