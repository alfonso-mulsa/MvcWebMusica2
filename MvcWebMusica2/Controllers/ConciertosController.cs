﻿using System;
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
    public class ConciertosController(GrupoBContext context) : Controller
    {
        // GET: Conciertos
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.Conciertos.Include(c => c.Ciudades).Include(c => c.Giras);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Conciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conciertos == null)
            {
                return NotFound();
            }

            return View(conciertos);
        }

        // GET: Conciertos/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre");
            ViewData["GirasId"] = new SelectList(context.Giras, "Id", "Nombre");
            return View();
        }

        // POST: Conciertos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (ModelState.IsValid)
            {
                context.Add(conciertos);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await context.Conciertos.FindAsync(id);
            if (conciertos == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (id != conciertos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(conciertos);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertosExists(conciertos.Id))
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
            ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conciertos == null)
            {
                return NotFound();
            }

            return View(conciertos);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conciertos = await context.Conciertos.FindAsync(id);
            if (conciertos != null)
            {
                context.Conciertos.Remove(conciertos);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosExists(int id)
        {
            return context.Conciertos.Any(e => e.Id == id);
        }

        //[HttpGet]
        //public async Task<FileResult> DescargarExcel()
        //{
        //    var conciertos = await repositorioConciertos.DameTodos();
        //    var nombreArchivo = $"Conciertos.xlsx";
        //    return GenerarExcel(nombreArchivo, conciertos);
        //}

        //private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Conciertos> conciertos)
        //{
        //    DataTable dataTable = new DataTable("Conciertos");
        //    dataTable.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("Fecha"),
        //        new DataColumn("Direccion"),
        //        new DataColumn("Ciudades"),
        //        new DataColumn("Giras")
        //    });

        //    foreach (var concierto in conciertos)
        //    {
        //        dataTable.Rows.Add(
        //            concierto.Fecha,
        //            concierto.Direccion,
        //            concierto.Ciudades.Nombre,
        //            concierto.Giras.Nombre);
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
