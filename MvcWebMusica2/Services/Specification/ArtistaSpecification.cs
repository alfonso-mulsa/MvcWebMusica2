
using MvcWebMusica2.Models;


namespace MvcWebMusica2.Services.Specification
{
    public class ArtistaSpecification (int ArtistaId): IArtistaSpecification
    {
        public bool IsValid(Artistas element)
        {
            return element.Id == ArtistaId;
        }
    }
}