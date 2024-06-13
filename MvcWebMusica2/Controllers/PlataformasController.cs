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
    public class PlataformasController(GrupoBContext context) : Controller
    {
        // GET: Plataformas
        public async Task<IActionResult> Index()
        {
            return View(await context.Plataformas.ToListAsync());
        }

        // GET: Plataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataformas == null)
            {
                return NotFound();
            }

            return View(plataformas);
        }

        // GET: Plataformas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Plataformas plataformas)
        {
            if (ModelState.IsValid)
            {
                context.Add(plataformas);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plataformas);
        }

        // GET: Plataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await context.Plataformas.FindAsync(id);
            if (plataformas == null)
            {
                return NotFound();
            }
            return View(plataformas);
        }

        // POST: Plataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Plataformas plataformas)
        {
            if (id != plataformas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(plataformas);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlataformasExists(plataformas.Id))
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
            return View(plataformas);
        }

        // GET: Plataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plataformas == null)
            {
                return NotFound();
            }

            return View(plataformas);
        }

        // POST: Plataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plataformas = await context.Plataformas.FindAsync(id);
            if (plataformas != null)
            {
                context.Plataformas.Remove(plataformas);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformasExists(int id)
        {
            return context.Plataformas.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var plataformas = await repositorioPlataformas.DameTodos();
            var nombreArchivo = $"Plataformas.xlsx";
            return GenerarExcel(nombreArchivo, plataformas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Plataformas> plataformas)
        {
            DataTable dataTable = new DataTable("Plataformas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nombre")
            });

            foreach (var plataforma in plataformas)
            {
                dataTable.Rows.Add(
                    plataforma.Nombre);
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
