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
    public class VideoClipsController(GrupoBContext context) : Controller
    {
        // GET: VideoClips
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.VideoClips.Include(v => v.Canciones);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: VideoClips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await context.VideoClips
                .Include(v => v.Canciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClips == null)
            {
                return NotFound();
            }

            return View(videoClips);
        }

        // GET: VideoClips/Create
        public IActionResult Create()
        {
            ViewData["CancionesId"] = new SelectList(context.Canciones, "Id", "Titulo");
            return View();
        }

        // POST: VideoClips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancionesId,Fecha")] VideoClips videoClips)
        {
            if (ModelState.IsValid)
            {
                context.Add(videoClips);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CancionesId"] = new SelectList(context.Canciones, "Id", "Titulo", videoClips.CancionesId);
            return View(videoClips);
        }

        // GET: VideoClips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await context.VideoClips.FindAsync(id);
            if (videoClips == null)
            {
                return NotFound();
            }
            ViewData["CancionesId"] = new SelectList(context.Canciones, "Id", "Titulo", videoClips.CancionesId);
            return View(videoClips);
        }

        // POST: VideoClips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancionesId,Fecha")] VideoClips videoClips)
        {
            if (id != videoClips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(videoClips);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoClipsExists(videoClips.Id))
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
            ViewData["CancionesId"] = new SelectList(context.Canciones, "Id", "Titulo", videoClips.CancionesId);
            return View(videoClips);
        }

        // GET: VideoClips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await context.VideoClips
                .Include(v => v.Canciones)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoClips == null)
            {
                return NotFound();
            }

            return View(videoClips);
        }

        // POST: VideoClips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoClips = await context.VideoClips.FindAsync(id);
            if (videoClips != null)
            {
                context.VideoClips.Remove(videoClips);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipsExists(int id)
        {
            return context.VideoClips.Any(e => e.Id == id);
        }
    }
}
