
using MvcWebMusica2.Models;


namespace MvcWebMusica2.Services.Specification
{
    public class ArtistaSpecification (int artistaId): IArtistaSpecification
    {
        public bool IsValid(Artistas element)
        {
            return element.Id == artistaId;
        }
    }
}