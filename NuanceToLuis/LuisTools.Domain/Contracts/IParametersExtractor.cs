using LuisTools.Domain.Entities;

namespace LuisTools.Domain.Contracts
{
    public interface IParametersExtractor
    {
        ProgramParameters Extract(params string[] args);
    }
}
