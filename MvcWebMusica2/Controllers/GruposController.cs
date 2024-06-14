﻿using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class GruposController(
        IGenericRepositorio<Grupos> repositorioGrupos,
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Representantes> repositorioRepresentantes) 
        : Controller
    {
        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            //var grupoBContext = context.Grupos.Include(g => g.Ciudades).Include(g => g.Generos).Include(g => g.Representantes);
            //return View(await grupoBContext.ToListAsync());
            var listaGrupos = await repositorioGrupos.DameTodos();
            foreach (var grupo in listaGrupos)
            {
                grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
                grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
                grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);
            }
            return View(listaGrupos);
        }

        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var grupos = await context.Grupos
            //    .Include(g => g.Ciudades)
            //    .Include(g => g.Generos)
            //    .Include(g => g.Representantes)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (grupos == null)
            //{
            //    return NotFound();
            //}

            //return View(grupos);

            if (id == null)
            {
                return NotFound();
            }

            var grupo = await repositorioGrupos.DameUno(id);

            if (grupo == null)
            {
                return NotFound();
            }

            grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
            grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
            grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);
            return View(grupo);
        }

        // GET: Grupos/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre");
            //ViewData["GenerosId"] = new SelectList(context.Generos, "Id", "Nombre");
            //ViewData["RepresentantesId"] = new SelectList(context.Representantes, "Id", "NombreCompleto");
            //return View();

            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Add(grupos);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(context.Generos, "Id", "Nombre", grupos.GenerosId);
            //ViewData["RepresentantesId"] = new SelectList(context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            //return View(grupos);

            if (ModelState.IsValid)
            {
                await repositorioGrupos.Agregar(grupos);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View(grupos);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var grupos = await context.Grupos.FindAsync(id);
            //if (grupos == null)
            //{
            //    return NotFound();
            //}
            //ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(context.Generos, "Id", "Nombre", grupos.GenerosId);
            //ViewData["RepresentantesId"] = new SelectList(context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            //return View(grupos);

            if (id == null)
            {
                return NotFound();
            }

            var grupo = await repositorioGrupos.DameUno(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            //if (id != grupos.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        context.Update(grupos);
            //        await context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!GruposExists(grupos.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CiudadesId"] = new SelectList(context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            //ViewData["GenerosId"] = new SelectList(context.Generos, "Id", "Nombre", grupos.GenerosId);
            //ViewData["RepresentantesId"] = new SelectList(context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            //return View(grupos);

            if (id != grupos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioGrupos.Modificar(id, grupos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruposExists(grupos.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View(grupos);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var grupos = await context.Grupos
            //    .Include(g => g.Ciudades)
            //    .Include(g => g.Generos)
            //    .Include(g => g.Representantes)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (grupos == null)
            //{
            //    return NotFound();
            //}

            //return View(grupos);

            if (id == null)
            {
                return NotFound();
            }

            var grupo = await repositorioGrupos.DameUno(id);

            if (grupo == null)
            {
                return NotFound();
            }

            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var grupos = await context.Grupos.FindAsync(id);
            //if (grupos != null)
            //{
            //    context.Grupos.Remove(grupos);
            //}

            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            var grupos = await repositorioGrupos.DameUno(id);
            if (grupos != null)
            {
                await repositorioGrupos.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GruposExists(int id)
        {
            //return context.Grupos.Any(e => e.Id == id);
            return repositorioGrupos.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var grupos = await repositorioGrupos.DameTodos();
            foreach (var grupo in grupos)
            {
                grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
                grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
                grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);
            }
            var nombreArchivo = "Grupos.xlsx";
            return GenerarExcel(nombreArchivo, grupos);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Grupos> grupos)
        {
            DataTable dataTable = new DataTable("Grupos");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Nombre"),
                new("FechaCreacion"),
                new("Ciudades"),
                new("Géneros"),
                new("Representantes")
            });

            foreach (var grupo in grupos)
            {
                dataTable.Rows.Add(
                    grupo.Nombre,
                    grupo.FechaCreacion,
                    grupo.Ciudades?.Nombre,
                    grupo.Generos?.Nombre,
                    grupo.Representantes?.NombreCompleto);
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
