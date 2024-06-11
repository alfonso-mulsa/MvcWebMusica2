using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class ArtistaSpecification (int ArtistaId): IGrupoSpecification
    {
        public bool IsValid(Grupos element)
        {
            return element.Id == ArtistaId;
        }
    }
}
