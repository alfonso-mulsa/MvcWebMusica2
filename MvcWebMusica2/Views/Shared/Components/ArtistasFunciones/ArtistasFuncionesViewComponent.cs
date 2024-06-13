using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MvcWebMusica2.Views.Shared.Components
{
    public class ArtistasFuncionesViewComponent (IGenericRepositorio<FuncionesArtistas> coleccion): ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IFuncionSpecification especificacion)
        {
            var items = await coleccion.DameTodos();
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
