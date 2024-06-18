﻿using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class EmpleadosController(
        IGenericRepositorio<Empleados> repositorioEmpleados,
        IGenericRepositorio<Roles> repositorioRoles
        ) : Controller
    {
        private readonly string descripcion = "Descripcion";
        private readonly string rolesId = "RolesId";

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var listaEmpleados = await repositorioEmpleados.DameTodos();
            foreach (var empleado in listaEmpleados)
            {
                empleado.Roles = await repositorioRoles.DameUno(empleado.RolesId);
            }
            return View(listaEmpleados);
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await repositorioEmpleados.DameUno(id);

            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                empleado.Roles = await repositorioRoles.DameUno(empleado.RolesId);
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[rolesId] = new SelectList(await repositorioRoles.DameTodos(), "Id", descripcion);
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,RolesId")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                await repositorioEmpleados.Agregar(empleados);
                return RedirectToAction(nameof(Index));
            }
            ViewData[rolesId] = new SelectList(await repositorioRoles.DameTodos(), "Id", descripcion, empleados.RolesId);
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await repositorioEmpleados.DameUno(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData[rolesId] = new SelectList(await repositorioRoles.DameTodos(), "Id", descripcion, empleado.RolesId);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,RolesId")] Empleados empleados)
        {
            if (id != empleados.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioEmpleados.Modificar(id, empleados);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosExists(empleados.Id))
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
            ViewData[rolesId] = new SelectList(await repositorioRoles.DameTodos(), "Id", descripcion, empleados.RolesId);
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await repositorioEmpleados.DameUno(id);

            if (empleado == null)
            {
                return NotFound();
            }
            else
            {
                empleado.Roles = await repositorioRoles.DameUno(empleado.RolesId);
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await repositorioEmpleados.DameUno(id);
            if (album != null)
            {
                await repositorioEmpleados.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadosExists(int id)
        {
            //return context.Empleados.Any(e => e.Id == id);

             return repositorioEmpleados.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var empleados = await repositorioEmpleados.DameTodos();
            foreach (var empleado in empleados)
            {
                empleado.Roles = await repositorioRoles.DameUno(empleado.RolesId);
            }
            var nombreArchivo = "Empleados.xlsx";
            return GenerarExcel(nombreArchivo, empleados);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<Empleados> empleados)
        {
            DataTable dataTable = new DataTable("Empleados");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("NombreCompleto"),
                new("Roles")
            });

            foreach (var empleado in empleados)
            {
                dataTable.Rows.Add(
                    empleado.NombreCompleto,
                    empleado.Roles?.Descripcion);
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
