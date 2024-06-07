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
    public class ArtistasController : Controller
    {
        //private readonly GrupoBContext _context;
        private readonly IGenericRepositorio<Artistas> _repositorioArtistas;
        private readonly IGenericRepositorio<Ciudades> _repositorioCiudades;
        private readonly IGenericRepositorio<Generos> _repositorioGeneros;
        private readonly IGenericRepositorio<Grupos> _repositorioGrupos;
        private readonly IGenericRepositorio<Funciones> _repositorioFunciones;
        private readonly IGenericRepositorio<FuncionesArtistas> _repositorioFuncionesArtistas;

        public ArtistasController(IGenericRepositorio<Artistas> repositorioArtistas, IGenericRepositorio<Ciudades> repositorioCiudades,
            IGenericRepositorio<Generos> repositorioGeneros, IGenericRepositorio<Grupos> repositorioGrupos,
            IGenericRepositorio<Funciones> repositorioFunciones, IGenericRepositorio<FuncionesArtistas> repositorioFuncionesArtistas)
        {
            _repositorioArtistas = repositorioArtistas;
            _repositorioCiudades = repositorioCiudades;
            _repositorioGeneros = repositorioGeneros;
            _repositorioGrupos = repositorioGrupos;
            _repositorioFunciones = repositorioFunciones;
            _repositorioFuncionesArtistas = repositorioFuncionesArtistas;
        }

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            var listaArtistas = _repositorioArtistas.DameTodos();
            List<FuncionesArtistas> listaFuncionesArtista = new();
            foreach (var artista in listaArtistas)
            {
                artista.Ciudades = _repositorioCiudades.DameUno(artista.CiudadesId);
                artista.Generos = _repositorioGeneros.DameUno(artista.GenerosId);
                artista.Grupos = _repositorioGrupos.DameUno(artista.GruposId);
                listaFuncionesArtista = _repositorioFuncionesArtistas.Filtra(x => x.ArtistasId == artista.Id);
                foreach (var funcionArtista in listaFuncionesArtista)
                {
                    funcionArtista.Funciones = _repositorioFunciones.DameUno(funcionArtista.FuncionesId);
                }

                artista.FuncionesArtistas = listaFuncionesArtista;
            }
            return View(listaArtistas);
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = _repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = _repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = _repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = _repositorioGrupos.DameUno(artista.GruposId);
            List<FuncionesArtistas> listaFuncionesArtista = _repositorioFuncionesArtistas.Filtra(x => x.ArtistasId == artista.Id);
            foreach (var funcionArtista in listaFuncionesArtista)
            {
                funcionArtista.Funciones = _repositorioFunciones.DameUno(funcionArtista.FuncionesId);
            }
            artista.FuncionesArtistas = listaFuncionesArtista;

            return View(artista);
        }

        // GET: Artistas/Create
        public IActionResult Create()
        {
            ViewData["CiudadesId"] = new SelectList(_repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (ModelState.IsValid)
            {
                _repositorioArtistas.Agregar(artista);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(_repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = _repositorioArtistas.DameUno(id);
            
            if (artista == null)
            {
                return NotFound();
            }

            ViewData["CiudadesId"] = new SelectList(_repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositorioArtistas.Modificar(id, artista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistasExists(artista.Id))
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
            ViewData["CiudadesId"] = new SelectList(_repositorioCiudades.DameTodos(), "Id", "Nombre", artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(_repositorioGeneros.DameTodos(), "Id", "Nombre", artista.GenerosId);
            ViewData["GruposId"] = new SelectList(_repositorioGrupos.DameTodos(), "Id", "Nombre", artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = _repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = _repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = _repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = _repositorioGrupos.DameUno(artista.GruposId);
            List<FuncionesArtistas> listaFuncionesArtista = _repositorioFuncionesArtistas.Filtra(x => x.ArtistasId == artista.Id);
            foreach (var funcionArtista in listaFuncionesArtista)
            {
                funcionArtista.Funciones = _repositorioFunciones.DameUno(funcionArtista.FuncionesId);
            }
            artista.FuncionesArtistas = listaFuncionesArtista;

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = _repositorioArtistas.DameUno(id);
            if (artista != null)
            {
                _repositorioArtistas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ArtistasExists(int id)
        {
            return _repositorioArtistas.DameUno(id) != null;
        }
    }
}
