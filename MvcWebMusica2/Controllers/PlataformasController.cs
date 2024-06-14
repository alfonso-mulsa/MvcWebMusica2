﻿using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class PlataformasController(
        IGenericRepositorio<Plataformas> repositorioPlataformas
        ) : Controller
    {
        // GET: Plataformas
        public async Task<IActionResult> Index()
        {
            return View(await repositorioPlataformas.DameTodos());
        }

        // GET: Plataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await repositorioPlataformas.DameUno(id);
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
        
        //public async Task<IActionResult> Create()
        //{
        //    return View();
        //}

        // POST: Plataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Create([Bind("Id,Nombre")] Plataformas plataformas)
        {
            if (ModelState.IsValid)
            {
                repositorioPlataformas.Agregar(plataformas);
                return Task.FromResult<IActionResult>(RedirectToAction(nameof(Index)));
            }
            return Task.FromResult<IActionResult>(View(plataformas));
        }

        //public async Task<IActionResult> Create([Bind("Id,Nombre")] Plataformas plataformas)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repositorioPlataformas.Agregar(plataformas);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(plataformas);
        //}

        // GET: Plataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await repositorioPlataformas.DameUno(id);
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
                    await repositorioPlataformas.Modificar(id, plataformas);
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

            var plataformas = await repositorioPlataformas.DameUno(id);
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
            var plataformas = await repositorioPlataformas.DameUno(id);
            if (plataformas != null)
            {
                await repositorioPlataformas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PlataformasExists(int id)
        {
            return repositorioPlataformas.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var plataformas = await repositorioPlataformas.DameTodos();
            var nombreArchivo = "Plataformas.xlsx";
            return GenerarExcel(nombreArchivo, plataformas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Plataformas> plataformas)
        {
            DataTable dataTable = new DataTable("Plataformas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Nombre")
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
