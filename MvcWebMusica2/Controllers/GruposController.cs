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
    public class GruposController : Controller
    {
        private readonly GrupoBContext _context;

        public GruposController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.Grupos.Include(g => g.Ciudades).Include(g => g.Generos).Include(g => g.Representantes);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos
                .Include(g => g.Ciudades)
                .Include(g => g.Generos)
                .Include(g => g.Representantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupos == null)
            {
                return NotFound();
            }

            return View(grupos);
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "NombreCompleto");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grupos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Nombre", grupos.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            return View(grupos);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos.FindAsync(id);
            if (grupos == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Nombre", grupos.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            return View(grupos);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            if (id != grupos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grupos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruposExists(grupos.Id))
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
            ViewData["CiudadesId"] = new SelectList(_context.Ciudades, "Id", "Nombre", grupos.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_context.Generos, "Id", "Nombre", grupos.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(_context.Representantes, "Id", "NombreCompleto", grupos.RepresentantesId);
            return View(grupos);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupos = await _context.Grupos
                .Include(g => g.Ciudades)
                .Include(g => g.Generos)
                .Include(g => g.Representantes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grupos == null)
            {
                return NotFound();
            }

            return View(grupos);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupos = await _context.Grupos.FindAsync(id);
            if (grupos != null)
            {
                _context.Grupos.Remove(grupos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GruposExists(int id)
        {
            return _context.Grupos.Any(e => e.Id == id);
        }
    }
}
