using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;



namespace MvcWebMusica2.Views.Shared.Components
{
    public class GenericoViewComponent (IGenericRepositorio<Grupos> coleccion): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int ArtistaId)
        {
            var items = await coleccion.DameTodos();
            IGrupoSpecification especificacion = new ArtistaSpecification(ArtistaId);
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
