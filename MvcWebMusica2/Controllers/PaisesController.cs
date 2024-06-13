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
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class PaisesController(
        IGenericRepositorio<Paises> repositorioPaises
        ) : Controller
    {
        // GET: Paises
        public async Task<IActionResult> Index()
        {
            return View(await repositorioPaises.DameTodos());
        }

        // GET: Paises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await repositorioPaises.DameUno(id);
            if (paises == null)
            {
                return NotFound();
            }

            return View(paises);
        }

        // GET: Paises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Paises paises)
        {
            if (ModelState.IsValid)
            {
                await repositorioPaises.Agregar(paises);
                return RedirectToAction(nameof(Index));
            }
            return View(paises);
        }

        // GET: Paises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await repositorioPaises.DameUno(id);
            if (paises == null)
            {
                return NotFound();
            }
            return View(paises);
        }

        // POST: Paises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Paises paises)
        {
            if (id != paises.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioPaises.Modificar(id, paises);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaisesExists(paises.Id))
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
            return View(paises);
        }

        // GET: Paises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paises = await repositorioPaises.DameUno(id);
            if (paises == null)
            {
                return NotFound();
            }

            return View(paises);
        }

        // POST: Paises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paises = await repositorioPaises.DameUno(id);
            if (paises != null)
            {
                await repositorioPaises.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaisesExists(int id)
        {
            return repositorioPaises.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel(int id)
        {
            var paises = await repositorioPaises.DameTodos();

            var nombreArchivo = $"Paises.xlsx";
            return GenerarExcel(nombreArchivo, paises);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Paises> paises)
        {
            DataTable dataTable = new DataTable("Paises");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nombre")
            });

            foreach (var pais in paises)
            {
                dataTable.Rows.Add(
                    pais.Nombre);
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
