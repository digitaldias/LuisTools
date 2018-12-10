using System.Xml.Linq;

namespace LuisTools.Domain.Contracts
{
    public interface ITrsxLoader
    {
        XElement Load(string fileName);
    }
}
