using LuisTools.Domain.Entities;
using System.Xml.Linq;

namespace LuisTools.Domain.Contracts
{
    public interface ILuDownMaker
    {
        string MakeFromTrsx(string sourceFileName, XElement trsxElement, ProgramParameters parameters);
    }
}
