﻿using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class CiudadesController(
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Paises> repositorioPaises
        ) : Controller
    {
        private readonly string nombre = "Nombre";
        private readonly string paisesId = "PaisesID";

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var listaCiudades = await repositorioCiudades.DameTodos();
            foreach (var ciudad in listaCiudades)
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }
            return View(listaCiudades);
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);

            if (ciudad == null)
            {
                return NotFound();
            }
            else
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }

            return View(ciudad);
        }

        // GET: Ciudades/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", nombre);
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
                await repositorioCiudades.Agregar(ciudades);
                return RedirectToAction(nameof(Index));
            }
            ViewData[paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", nombre, ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            ViewData[paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", nombre, ciudad.PaisesID);
            return View(ciudad);
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
                    await repositorioCiudades.Modificar(id, ciudades);
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
            ViewData[paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", nombre, ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);

            if (ciudad == null)
            {
                return NotFound();
            }
            else
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }

            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudad = await repositorioCiudades.DameUno(id);
            if (ciudad != null)
            {
                await repositorioCiudades.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CiudadesExists(int id)
        {
            return repositorioCiudades.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var ciudades = await repositorioCiudades.DameTodos();
            foreach (var ciudad in ciudades)
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }
            var nombreArchivo = "Ciudades.xlsx";
            return GenerarExcel(nombreArchivo, ciudades);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Ciudades> ciudades)
        {
            DataTable dataTable = new DataTable("Ciudades");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Nombre"),
                new("Paises"),
            });

            foreach (var ciudad in ciudades)
            {
                dataTable.Rows.Add(
                    ciudad.Nombre,
                    ciudad.Paises?.Nombre);
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
