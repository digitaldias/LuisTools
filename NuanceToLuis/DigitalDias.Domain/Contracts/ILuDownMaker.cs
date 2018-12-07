using DigitalDias.Domain.Entities;
using System.Xml.Linq;

namespace DigitalDias.Domain.Contracts
{
    public interface ILuDownMaker
    {
        string MakeFromTrsx(string sourceFileName, XElement trsxElement, ProgramParameters parameters);
    }
}
