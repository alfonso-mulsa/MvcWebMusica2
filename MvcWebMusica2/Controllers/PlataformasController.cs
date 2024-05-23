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
    public class PlataformasController : Controller
    {
        private readonly GrupoBContext _context;

        public PlataformasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Plataformas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plataformas.ToListAsync());
        }

        // GET: Plataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await _context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
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

        // POST: Plataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Plataformas plataformas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plataformas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plataformas);
        }

        // GET: Plataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataformas = await _context.Plataformas.FindAsync(id);
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
                    _context.Update(plataformas);
                    await _context.SaveChangesAsync();
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

            var plataformas = await _context.Plataformas
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var plataformas = await _context.Plataformas.FindAsync(id);
            if (plataformas != null)
            {
                _context.Plataformas.Remove(plataformas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformasExists(int id)
        {
            return _context.Plataformas.Any(e => e.Id == id);
        }
    }
}
