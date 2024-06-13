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
    public class FuncionesController(
        IGenericRepositorio<Funciones> repositorioFunciones
        ) : Controller
    {
        // GET: Funciones
        public async Task<IActionResult> Index()
        {
            //return View(await context.Funciones.ToListAsync());

            var listaFunciones = await repositorioFunciones.DameTodos();
            return View(listaFunciones);
        }

        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var funciones = await context.Funciones
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (funciones == null)
            //{
            //    return NotFound();
            //}

            //return View(funciones);

            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);

            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // GET: Funciones/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Funciones funciones)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Add(funciones);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(funciones);

            if (ModelState.IsValid)
            {
                await repositorioFunciones.Agregar(funciones);
                return RedirectToAction(nameof(Index));
            }

            return View(funciones);
        }

        // GET: Funciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var funciones = await context.Funciones.FindAsync(id);
            //if (funciones == null)
            //{
            //    return NotFound();
            //}
            //return View(funciones);

            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);
            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // POST: Funciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Funciones funciones)
        {
            //if (id != funciones.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        context.Update(funciones);
            //        await context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!FuncionesExists(funciones.Id))
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
            //return View(funciones);

            if (id != funciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioFunciones.Modificar(id, funciones);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionesExists(funciones.Id))
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

            return View(funciones);
        }

        // GET: Funciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var funciones = await context.Funciones
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (funciones == null)
            //{
            //    return NotFound();
            //}

            //return View(funciones);

            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);

            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var funciones = await context.Funciones.FindAsync(id);
            //if (funciones != null)
            //{
            //    context.Funciones.Remove(funciones);
            //}

            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            var funciones = await repositorioFunciones.DameUno(id);
            if (funciones != null)
            {
                await repositorioFunciones.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FuncionesExists(int id)
        {
            //return context.Funciones.Any(e => e.Id == id);

            return repositorioFunciones.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var funciones = await repositorioFunciones.DameTodos();
            var nombreArchivo = $"Funciones.xlsx";
            return GenerarExcel(nombreArchivo, funciones);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Funciones> funciones)
        {
            DataTable dataTable = new DataTable("Funciones");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Nombre")
            });

            foreach (var funcion in funciones)
            {
                dataTable.Rows.Add(
                    funcion.Nombre);
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
