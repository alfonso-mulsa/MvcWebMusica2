﻿using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class RolesController(
        IGenericRepositorio<Roles> repositorioRoles
        ) : Controller
    {
        // GET: Roles
        public async Task<IActionResult> Index()
        {
            return View(await repositorioRoles.DameTodos());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await repositorioRoles.DameUno(id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // GET: Roles/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion")] Roles roles)
        {
            if (ModelState.IsValid)
            {
                await repositorioRoles.Agregar(roles);
                return RedirectToAction(nameof(Index));
            }
            return View(roles);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await repositorioRoles.DameUno(id);
            if (roles == null)
            {
                return NotFound();
            }
            return View(roles);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion")] Roles roles)
        {
            if (id != roles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioRoles.Modificar(id, roles);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesExists(roles.Id))
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
            return View(roles);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roles = await repositorioRoles.DameUno(id);
            if (roles == null)
            {
                return NotFound();
            }

            return View(roles);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roles = await repositorioRoles.DameUno(id);
            if (roles != null)
            {
                await repositorioRoles.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RolesExists(int id)
        {
            return repositorioRoles.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var roles = await repositorioRoles.DameTodos();
            var nombreArchivo = $"Roles.xlsx";
            return GenerarExcel(nombreArchivo, roles);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Roles> roles)
        {
            DataTable dataTable = new DataTable("Roles");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Descripcion")
            });

            foreach (var rol in roles)
            {
                dataTable.Rows.Add(
                    rol.Descripcion);
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
