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
    public class GirasController : Controller
    {
        private readonly GrupoBContext _context;

        public GirasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Giras
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.Giras.Include(g => g.Grupos);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Giras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await _context.Giras
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
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre");
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
                _context.Add(giras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await _context.Giras.FindAsync(id);
            if (giras == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", giras.GruposId);
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
                    _context.Update(giras);
                    await _context.SaveChangesAsync();
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
            ViewData["GruposId"] = new SelectList(_context.Grupos, "Id", "Nombre", giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await _context.Giras
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
            var giras = await _context.Giras.FindAsync(id);
            if (giras != null)
            {
                _context.Giras.Remove(giras);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GirasExists(int id)
        {
            return _context.Giras.Any(e => e.Id == id);
        }
    }
}
