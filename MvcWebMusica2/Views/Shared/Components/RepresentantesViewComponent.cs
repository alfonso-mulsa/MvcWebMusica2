using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;
using System.Linq;

namespace MvcWebMusica2.Views.Shared.Components
{
    public class RepresentantesViewComponent (IGenericRepositorio<Grupos> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int representanteId) //IGrupoSpecification especificacion
        {
            //IEnumerable<Grupos> coleccionInicial = await coleccion.DameTodos();
            //if (especificacion is not null)
            //{
            //    coleccionInicial = coleccion.Where(especificacion.IsValid);
            //}

            //return View();

            var items = await coleccion.DameTodos();
            IGrupoSpecification especificacion = new RepresentanteSpecification(representanteId);
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
