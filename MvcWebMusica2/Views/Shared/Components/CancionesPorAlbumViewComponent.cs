using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;

namespace MvcWebMusica2.Views.Shared.Components
{
    public class CancionesPorAlbumViewComponent(IGenericRepositorio<Canciones> repositorioCanciones) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int AlbumId)
        {
            var listaCanciones = await repositorioCanciones.DameTodos();
            ICancionSpecification filtroCanciones = new CancionesPorAlbumSpecification(AlbumId);
            var cancionesFiltradas = listaCanciones.Where(filtroCanciones.IsValid);
            return View(cancionesFiltradas);
        }
    }
}
