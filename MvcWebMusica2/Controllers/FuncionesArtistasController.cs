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
    public class FuncionesArtistasController : Controller
    {
        private readonly GrupoBContext _context;

        public FuncionesArtistasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: FuncionesArtistas
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.FuncionesArtistas.Include(f => f.Artistas).Include(f => f.Funciones);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: FuncionesArtistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await _context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }

            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Create
        public IActionResult Create()
        {
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre");
            ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Nombre");
            return View();
        }

        // POST: FuncionesArtistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FuncionesId,ArtistasId")] FuncionesArtistas funcionesArtistas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionesArtistas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await _context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // POST: FuncionesArtistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FuncionesId,ArtistasId")] FuncionesArtistas funcionesArtistas)
        {
            if (id != funcionesArtistas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionesArtistas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionesArtistasExists(funcionesArtistas.Id))
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
            ViewData["ArtistasId"] = new SelectList(_context.Artistas, "Id", "Nombre", funcionesArtistas.ArtistasId);
            ViewData["FuncionesId"] = new SelectList(_context.Funciones, "Id", "Nombre", funcionesArtistas.FuncionesId);
            return View(funcionesArtistas);
        }

        // GET: FuncionesArtistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionesArtistas = await _context.FuncionesArtistas
                .Include(f => f.Artistas)
                .Include(f => f.Funciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionesArtistas == null)
            {
                return NotFound();
            }

            return View(funcionesArtistas);
        }

        // POST: FuncionesArtistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionesArtistas = await _context.FuncionesArtistas.FindAsync(id);
            if (funcionesArtistas != null)
            {
                _context.FuncionesArtistas.Remove(funcionesArtistas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionesArtistasExists(int id)
        {
            return _context.FuncionesArtistas.Any(e => e.Id == id);
        }
    }
}
