using System.Xml.Linq;

namespace DigitalDias.Domain.Contracts
{
    public interface ITrsxLoader
    {
        XElement Load(string fileName);
    }
}
