using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class GenerosController : Controller
    {
        //private readonly GrupoBContext _context;
        private readonly IGenericRepositorio<Generos> _repositorioGeneros;

        public GenerosController(IGenericRepositorio<Generos> repositorioGeneros)
        {
            _repositorioGeneros = repositorioGeneros;
        }

        // GET: Generos
        public async Task<IActionResult> Index()
        {
            return View(await _repositorioGeneros.DameTodos());
        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await _repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }

            return View(generos);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Generos generos)
        {
            if (ModelState.IsValid)
            {
                await _repositorioGeneros.Agregar(generos);
                return RedirectToAction(nameof(Index));
            }
            return View(generos);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await _repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }
            return View(generos);
        }

        // POST: Generos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Generos generos)
        {
            if (id != generos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repositorioGeneros.Modificar(id, generos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenerosExists(generos.Id))
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
            return View(generos);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await _repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }

            return View(generos);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generos = await _repositorioGeneros.DameUno(id);
            if (generos != null)
            {
                await _repositorioGeneros.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GenerosExists(int id)
        {
            return _repositorioGeneros.DameUno(id) != null;
        }
    }
}
