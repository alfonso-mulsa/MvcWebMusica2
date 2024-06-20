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
    public class ProximosConciertoesController : Controller
    {
        private readonly GrupoBContext _context;

        public ProximosConciertoesController(GrupoBContext context)
        {
            _context = context;
        }




        // GET: ProximosConciertoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProximosConciertos.ToListAsync());
        }

        // GET: ProximosConciertoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proximosConcierto = await _context.ProximosConciertos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proximosConcierto == null)
            {
                return NotFound();
            }

            return View(proximosConcierto);
        }

        // GET: ProximosConciertoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProximosConciertoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Gira,Ciudad,Grupo")] ProximosConcierto proximosConcierto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proximosConcierto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proximosConcierto);
        }

        // GET: ProximosConciertoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proximosConcierto = await _context.ProximosConciertos.FindAsync(id);
            if (proximosConcierto == null)
            {
                return NotFound();
            }
            return View(proximosConcierto);
        }

        // POST: ProximosConciertoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,Gira,Ciudad,Grupo")] ProximosConcierto proximosConcierto)
        {
            if (id != proximosConcierto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proximosConcierto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProximosConciertoExists(proximosConcierto.Id))
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
            return View(proximosConcierto);
        }

        // GET: ProximosConciertoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proximosConcierto = await _context.ProximosConciertos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proximosConcierto == null)
            {
                return NotFound();
            }

            return View(proximosConcierto);
        }

        // POST: ProximosConciertoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proximosConcierto = await _context.ProximosConciertos.FindAsync(id);
            if (proximosConcierto != null)
            {
                _context.ProximosConciertos.Remove(proximosConcierto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProximosConciertoExists(int id)
        {
            return _context.ProximosConciertos.Any(e => e.Id == id);
        }
    }
}
