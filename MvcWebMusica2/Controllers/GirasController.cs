using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Controllers
{
    public class GirasController(GrupoBContext context) : Controller
    {
        // GET: Giras
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.Giras.Include(g => g.Grupos);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await context.Giras
                .Include(g => g.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giras == null)
            {
                return NotFound();
            }

            return View(giras);
        }

        // GET: Giras/Create
        public IActionResult Create()
        {
            ViewData["GruposId"] = new SelectList(context.Grupos, "Id", "Nombre");
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
                context.Add(giras);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(context.Grupos, "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await context.Giras.FindAsync(id);
            if (giras == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(context.Grupos, "Id", "Nombre", giras.GruposId);
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
                    context.Update(giras);
                    await context.SaveChangesAsync();
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
            ViewData["GruposId"] = new SelectList(context.Grupos, "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await context.Giras
                .Include(g => g.Grupos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (giras == null)
            {
                return NotFound();
            }

            return View(giras);
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giras = await context.Giras.FindAsync(id);
            if (giras != null)
            {
                context.Giras.Remove(giras);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GirasExists(int id)
        {
            return context.Giras.Any(e => e.Id == id);
        }
    }
}
