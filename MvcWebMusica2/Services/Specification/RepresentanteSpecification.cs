using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Specification;
using System.Xml.Linq;

namespace MvcWebMusica2.Services.Specification
{
    public class RepresentanteSpecification (int representanteId) : IGrupoSpecification
    {
        public bool IsValid(Grupos element)
        {
            return element.RepresentantesId == representanteId;
        }
    }
}