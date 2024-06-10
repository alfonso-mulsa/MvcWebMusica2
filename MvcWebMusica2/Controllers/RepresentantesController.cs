﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Controllers
{
    public class RepresentantesController(GrupoBContext context) : Controller
    {
        // GET: Representantes
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.Representantes.Include(r => r.Ciudades);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Representantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await context.Representantes
                .Include(r => r.Ciudades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (representantes == null)
            {
                return NotFound();
            }

            return View(representantes);
        }

        // GET: Representantes/Create
        public IActionResult Create()
        {
            ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre");
            return View();
        }

        // POST: Representantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            if (ModelState.IsValid)
            {
                context.Add(representantes);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await context.Representantes.FindAsync(id);
            if (representantes == null)
            {
                return NotFound();
            }
            ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // POST: Representantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            if (id != representantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(representantes);
                    await context.SaveChangesAsync();
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
            ViewData["CiudadesID"] = new SelectList(context.Ciudades, "Id", "Nombre", representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await context.Representantes
                .Include(r => r.Ciudades)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (representantes == null)
            {
                return NotFound();
            }

            return View(representantes);
        }

        // POST: Representantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representantes = await context.Representantes.FindAsync(id);
            if (representantes != null)
            {
                context.Representantes.Remove(representantes);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepresentantesExists(int id)
        {
            return context.Representantes.Any(e => e.Id == id);
        }
    }
}
