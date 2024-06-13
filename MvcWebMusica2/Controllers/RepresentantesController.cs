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
    public class RepresentantesController(
        IGenericRepositorio<Representantes> repositorioRepresentantes,
        IGenericRepositorio<Ciudades> repositorioCiudades) 
        : Controller
    {
        // GET: Representantes
        public async Task<IActionResult> Index()
        {
            //var grupoBContext = context.Representantes.Include(r => r.Ciudades);
            //return View(await grupoBContext.ToListAsync());

            var listaRepresentantes = await repositorioRepresentantes.DameTodos();
            foreach (var representante in listaRepresentantes)
            {
                representante.Ciudades = await repositorioCiudades.DameUno(representante.CiudadesID);
            }
            return View(listaRepresentantes);
        }

        // GET: Representantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var representantes = await context.Representantes
            //    .Include(r => r.Ciudades)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (representantes == null)
            //{
            //    return NotFound();
            //}

            //return View(representantes);

            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }

            representantes.Ciudades = await repositorioCiudades.DameUno(representantes.CiudadesID);

            return View(representantes);
        }

        // GET: Representantes/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre");
            //return View();

            ViewData["CiudadesID"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Representantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Add(representantes);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            //return View(representantes);

            if (ModelState.IsValid)
            {
                await repositorioRepresentantes.Agregar(representantes);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesID"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var representantes = await context.Representantes.FindAsync(id);
            //if (representantes == null)
            //{
            //    return NotFound();
            //}
            //ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            //return View(representantes);

            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }
            ViewData["CiudadesID"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // POST: Representantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            //if (id != representantes.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        context.Update(representantes);
            //        await context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!RepresentantesExists(representantes.Id))
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
            //ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            //return View(representantes);

            if (id != representantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioRepresentantes.Modificar(id, representantes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepresentantesExists(representantes.Id))
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
            ViewData["CiudadesID"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var representantes = await context.Representantes
            //    .Include(r => r.Ciudades)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (representantes == null)
            //{
            //    return NotFound();
            //}

            //return View(representantes);

            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }

            representantes.Ciudades = await repositorioCiudades.DameUno(representantes.CiudadesID);
            return View(representantes);
        }

        // POST: Representantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var representantes = await context.Representantes.FindAsync(id);
            //if (representantes != null)
            //{
            //    context.Representantes.Remove(representantes);
            //}

            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes != null)
            {
                await repositorioRepresentantes.Borrar(id);
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool RepresentantesExists(int id)
        {
            //return context.Representantes.Any(e => e.Id == id);

            return repositorioRepresentantes.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var representantes = await repositorioRepresentantes.DameTodos();
            var nombreArchivo = $"Representantes.xlsx";
            return GenerarExcel(nombreArchivo, representantes);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Representantes> representantes)
        {
            DataTable dataTable = new DataTable("Representantes");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("NombreCompleto"),
                new DataColumn("FechaNacimiento"),
                new DataColumn("Identificacion"),
                new DataColumn("Mail"),
                new DataColumn("Telefono"),
                new DataColumn("Ciudades")
            });

            foreach (var representante in representantes)
            {
                dataTable.Rows.Add(
                    representante.NombreCompleto,
                    representante.FechaNacimiento,
                    representante.Identificacion,
                    representante.mail,
                    representante.Telefono,
                    representante.Ciudades.Nombre);
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
