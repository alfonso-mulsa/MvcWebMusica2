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
    public class GirasController(
        IGenericRepositorio<Giras> repositorioGiras,
        IGenericRepositorio<Grupos> repositorioGrupos)
        : Controller
    {
        // GET: Giras
        public async Task<IActionResult> Index()
        {
            var listaGiras = await repositorioGiras.DameTodos();
            foreach (var giras in listaGiras)
            {
                giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);
            }
            return View(listaGiras);
        }

        // GET: Informacion de las Giras

        public async Task<IActionResult> InfoGiras()
        {
            var listaGiras = await repositorioGiras.DameTodos();
            foreach (var giras in listaGiras)
            {
                giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);
            }
            return View(listaGiras);
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await repositorioGiras.DameUno(id);
            if (giras == null)
            {
                return NotFound();
            }
            else
            {
                giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);
            }

            return View(giras);
        }

        // GET: Giras/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Giras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Giras giras)
        {
            if (ModelState.IsValid)
            {
                await repositorioGiras.Agregar(giras);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await repositorioGiras.DameUno(id);
            if (giras == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // POST: Giras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Giras giras)
        {
            if (id != giras.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioGiras.Modificar(id, giras);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GirasExists(giras.Id))
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
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await repositorioGiras.DameUno(id);
            if (giras == null)
            {
                return NotFound();
            }
            else
            {
                giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);
            }

            return View(giras);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giras = await repositorioGiras.DameUno(id);
            if (giras != null)
            {
                await repositorioGiras.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GirasExists(int id)
        {
            return repositorioGiras.DameUno(id) !=null;
        }

        // DESCARGAR EXCEL

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var giras = await repositorioGiras.DameTodos();
            var nombreArchivo = $"Giras.xlsx";
            return GenerarExcel(nombreArchivo, giras);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Giras> giras)
        {
            DataTable dataTable = new DataTable("Giras");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nombre"),
                new DataColumn("FechaInicio"),
                new DataColumn("FechaFin"),
                new DataColumn("Grupos")
            });

            foreach (var gira in giras)
            {
                dataTable.Rows.Add(
                    gira.Nombre,
                    gira.FechaInicio,
                    gira.FechaFin,
                    gira.Grupos);
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
