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
    public class VideoClipsPlataformasController : Controller
    {
        private readonly GrupoBContext _context;

        public VideoClipsPlataformasController(GrupoBContext context)
        {
            _context = context;
        }

        // GET: VideoClipsPlataformas
        public async Task<IActionResult> Index()
        {
            var grupoBContext = _context.VideoClipsPlataformas.Include(v => v.Plataformas).Include(v => v.VideoClips);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: VideoClipsPlataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await _context.VideoClipsPlataformas
                .Include(v => v.Plataformas)
                .Include(v => v.VideoClips)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }

            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Create
        public IActionResult Create()
        {
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Nombre");
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id");
            return View();
        }

        // POST: VideoClipsPlataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videoClipsPlataformas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await _context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            if (id != videoClipsPlataformas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoClipsPlataformas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoClipsPlataformasExists(videoClipsPlataformas.Id))
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
            ViewData["PlataformasId"] = new SelectList(_context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(_context.VideoClips, "Id", "Id", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await _context.VideoClipsPlataformas
                .Include(v => v.Plataformas)
                .Include(v => v.VideoClips)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }

            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoClipsPlataformas = await _context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataformas != null)
            {
                _context.VideoClipsPlataformas.Remove(videoClipsPlataformas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipsPlataformasExists(int id)
        {
            return _context.VideoClipsPlataformas.Any(e => e.Id == id);
        }
    }
}
