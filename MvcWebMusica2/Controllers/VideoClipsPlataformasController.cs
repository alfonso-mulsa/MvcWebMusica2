using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Controllers
{
    public class VideoClipsPlataformasController(GrupoBContext context) : Controller
    {
        // GET: VideoClipsPlataformas
        public async Task<IActionResult> Index()
        {
            var grupoBContext = context.VideoClipsPlataformas.Include(v => v.Plataformas).Include(v => v.VideoClips);
            return View(await grupoBContext.ToListAsync());
        }

        // GET: VideoClipsPlataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await context.VideoClipsPlataformas
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
            ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre");
            ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones");
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
                context.Add(videoClipsPlataformas);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }
            ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
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
                    context.Update(videoClipsPlataformas);
                    await context.SaveChangesAsync();
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
            ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await context.VideoClipsPlataformas
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
            var videoClipsPlataformas = await context.VideoClipsPlataformas.FindAsync(id);
            if (videoClipsPlataformas != null)
            {
                context.VideoClipsPlataformas.Remove(videoClipsPlataformas);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipsPlataformasExists(int id)
        {
            return context.VideoClipsPlataformas.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameTodos();
            var nombreArchivo = $"VideoclipsPlataformas.xlsx";
            return GenerarExcel(nombreArchivo, videoClipsPlataformas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<VideoClipsPlataformas> videoClipsPlataformas)
        {
            DataTable dataTable = new DataTable("VideoClipsPlataformas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Url"),
                new DataColumn("Plataformas"),
                new DataColumn("VideClips")
            });

            foreach (var videoClipPlataformas in videoClipsPlataformas)
            {
                dataTable.Rows.Add(
                    videoClipPlataformas.Url,
                    videoClipPlataformas.Plataformas,
                    videoClipPlataformas.VideoClips);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nombreArchivo);
                }
            }
        }
    }
}
