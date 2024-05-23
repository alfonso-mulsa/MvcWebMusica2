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
    public class ConciertosController : Controller
    {
        private readonly GrupoBContext _context;

        public ConciertosController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Conciertos
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.Conciertos.Include(c => c.Ciudades).Include(c => c.Giras);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Conciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await _context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conciertos == null)
            {
                return NotFound();
            }

            return View(conciertos);
        }

        // GET: Conciertos/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre");
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre");
            return View();
        }

        // POST: Conciertos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conciertos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await _context.Conciertos.FindAsync(id);
            if (conciertos == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (id != conciertos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conciertos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConciertosExists(conciertos.Id))
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", conciertos.CiudadesId);
            ViewData["GirasId"] = new SelectList(_context.Giras, "Id", "Nombre", conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conciertos = await _context.Conciertos
                .Include(c => c.Ciudades)
                .Include(c => c.Giras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conciertos == null)
            {
                return NotFound();
            }

            return View(conciertos);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conciertos = await _context.Conciertos.FindAsync(id);
            if (conciertos != null)
            {
                _context.Conciertos.Remove(conciertos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConciertosExists(int id)
        {
            return _context.Conciertos.Any(e => e.Id == id);
        }
    }
}
