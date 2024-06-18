using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;

namespace MvcWebMusica2.Views.Shared.Components
{
    public class ArtistasViewComponent(IGenericRepositorio<Artistas> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int GrupoId)
        {
            var items = await coleccion.DameTodos();
            IArtistaSpecification especificacion = new GrupoSpecification(GrupoId);
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
