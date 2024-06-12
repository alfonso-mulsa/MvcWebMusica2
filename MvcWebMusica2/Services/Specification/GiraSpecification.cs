using MvcWebMusica2.Models;

namespace MvcWebMusica2.Services.Specification
{
    public class GiraSpecification(int GiraId) : IConciertoSpecification
    {
        public bool IsValid(Conciertos element)
        {
            return element.GirasId == GiraId;
        }
    }
}
