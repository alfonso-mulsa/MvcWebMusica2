using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;
using System.Linq;

namespace MvcWebMusica2.Views.Shared.Components
{
    public class RepresentantesViewComponent(IGenericRepositorio<Representantes> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int representanteId)
        {
            var items = await coleccion.DameTodos();
            IGrupoSpecification especificacion = new GrupoSpecification(representanteId);
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
