using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;

namespace MvcWebMusica2.Views.Shared.Components.Giras
{
    public class GirasViewComponent(IGenericRepositorio<Conciertos> coleccion,IGenericRepositorio<Ciudades> colCiudad) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int GiraId)
        {
            var items = await coleccion.DameTodos();
            IConciertoSpecification especificacion = new GiraSpecification(GiraId);
            foreach (var Concierto in items)
            {
                var ciudadEncontrada = await colCiudad.DameUno(Concierto.CiudadesId);
                if (ciudadEncontrada != null)
                    Concierto.Direccion = ciudadEncontrada.Nombre;
            }
            
            var itemsFiltrados = items.AsParallel().Where(especificacion.IsValid);


            return View(itemsFiltrados);
        }
    }
}
