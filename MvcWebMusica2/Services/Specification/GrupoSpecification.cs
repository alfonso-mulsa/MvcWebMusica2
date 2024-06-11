using MvcWebMusica2.Models;
using System.Xml.Linq;

namespace MvcWebMusica2.Services.Specification
{
    public class GrupoSpecification (int representanteId): IGrupoSpecification
    {
        public bool IsValid(Grupos element)
        {
            return element.RepresentantesId == representanteId;
        }
    }
}
