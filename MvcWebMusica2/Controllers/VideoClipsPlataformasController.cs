using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class VideoClipsPlataformasController(
        IGenericRepositorio<VideoClipsPlataformas> repositorioVideoClipsPlataformas,
        IGenericRepositorio<Plataformas> repositorioPlataformas,
        IGenericRepositorio<VideoClips> repositorioVideoClips,
        IGenericRepositorio<Canciones> repositorioCanciones
        ) : Controller
    {
        // GET: VideoClipsPlataformas
        public async Task<IActionResult> Index()
        {
            //var grupoBContext = context.VideoClipsPlataformas.Include(v => v.Plataformas).Include(v => v.VideoClips);
            //return View(await grupoBContext.ToListAsync());

            var listaVideoClipsPlataformas = await repositorioVideoClipsPlataformas.DameTodos();
            foreach (var videoClipPlataforma in listaVideoClipsPlataformas)
            {
                videoClipPlataforma.Plataformas = await repositorioPlataformas.DameUno(videoClipPlataforma.PlataformasId);
                videoClipPlataforma.VideoClips = await repositorioVideoClips.DameUno(videoClipPlataforma.VideoClipsId);
                videoClipPlataforma.VideoClips.Canciones = await repositorioCanciones.DameUno(videoClipPlataforma.VideoClips.CancionesId);
            }
            return View(listaVideoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var videoClipsPlataformas = await context.VideoClipsPlataformas
            //    .Include(v => v.Plataformas)
            //    .Include(v => v.VideoClips)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (videoClipsPlataformas == null)
            //{
            //    return NotFound();
            //}

            //return View(videoClipsPlataformas);

            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);

            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }
            else
            {
                videoClipsPlataformas.Plataformas = await repositorioPlataformas.DameUno(videoClipsPlataformas.PlataformasId);
                videoClipsPlataformas.VideoClips = await repositorioVideoClips.DameUno(videoClipsPlataformas.VideoClipsId);
                videoClipsPlataformas.VideoClips.Canciones = await repositorioCanciones.DameUno(videoClipsPlataformas.VideoClips.CancionesId);
            }

            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Create
        public async Task<IActionResult> CreateAsync()
        {
            //ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre");
            //ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones");
            //return View();

            ViewData["PlataformasId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre");
            ViewData["VideoClipsId"] = new SelectList(await repositorioVideoClips.DameTodos(), "Id", "Canciones");
            return View();
        }

        // POST: VideoClipsPlataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Add(videoClipsPlataformas);
            //    await context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            //return View(videoClipsPlataformas);

            if (ModelState.IsValid)
            {
                await repositorioVideoClipsPlataformas.Agregar(videoClipsPlataformas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlataformaId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipId"] = new SelectList(await repositorioVideoClips.DameTodos(), "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var videoClipsPlataformas = await context.VideoClipsPlataformas.FindAsync(id);
            //if (videoClipsPlataformas == null)
            //{
            //    return NotFound();
            //}
            //ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            //return View(videoClipsPlataformas);

            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }
            ViewData["PlataformaId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipId"] = new SelectList(await repositorioVideoClips.DameTodos(), "Id", "Nombre", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            //if (id != videoClipsPlataformas.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        context.Update(videoClipsPlataformas);
            //        await context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!VideoClipsPlataformasExists(videoClipsPlataformas.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["PlataformasId"] = new SelectList(context.Plataformas, "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            //ViewData["VideoClipsId"] = new SelectList(context.VideoClips, "Id", "Canciones", videoClipsPlataformas.VideoClipsId);
            //return View(videoClipsPlataformas);

            if (id != videoClipsPlataformas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioVideoClipsPlataformas.Modificar(id, videoClipsPlataformas);
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
            ViewData["PlataformaId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipId"] = new SelectList(await repositorioVideoClips.DameTodos(), "Id", "Nombre", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var videoClipsPlataformas = await context.VideoClipsPlataformas
            //    .Include(v => v.Plataformas)
            //    .Include(v => v.VideoClips)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (videoClipsPlataformas == null)
            //{
            //    return NotFound();
            //}

            //return View(videoClipsPlataformas);

            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);

            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }
            else
            {
                videoClipsPlataformas.Plataformas = await repositorioPlataformas.DameUno(videoClipsPlataformas.PlataformasId);
                videoClipsPlataformas.VideoClips = await repositorioVideoClips.DameUno(videoClipsPlataformas.VideoClipsId);
            }

            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var videoClipsPlataformas = await context.VideoClipsPlataformas.FindAsync(id);
            //if (videoClipsPlataformas != null)
            //{
            //    context.VideoClipsPlataformas.Remove(videoClipsPlataformas);
            //}

            //await context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));

            var album = await repositorioVideoClipsPlataformas.DameUno(id);
            if (album != null)
            {
                await repositorioVideoClipsPlataformas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VideoClipsPlataformasExists(int id)
        {
            //return context.VideoClipsPlataformas.Any(e => e.Id == id);

            return repositorioVideoClipsPlataformas.DameUno(id) != null;
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameTodos();
            foreach (var videoClipPlataforma in videoClipsPlataformas)
            {
                videoClipPlataforma.Plataformas = await repositorioPlataformas.DameUno(videoClipPlataforma.PlataformasId);
                videoClipPlataforma.VideoClips = await repositorioVideoClips.DameUno(videoClipPlataforma.VideoClipsId);
            }
            var nombreArchivo = "VideoclipsPlataformas.xlsx";
            return GenerarExcel(nombreArchivo, videoClipsPlataformas);
        }

        private FileResult GenerarExcel(string nombreArchivo, IEnumerable<VideoClipsPlataformas> videoClipsPlataformas)
        {
            DataTable dataTable = new DataTable("VideoClipsPlataformas");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new("Url"),
                new("Plataformas"),
                new("VideClips")
            });

            foreach (var videoClipPlataformas in videoClipsPlataformas)
            {
                dataTable.Rows.Add(
                    videoClipPlataformas.url,
                    videoClipPlataformas.Plataformas?.Nombre,
                    videoClipPlataformas.VideoClips?.Id);
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
