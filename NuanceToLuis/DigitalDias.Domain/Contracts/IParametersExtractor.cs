using DigitalDias.Domain.Entities;

namespace DigitalDias.Domain.Contracts
{
    public interface IParametersExtractor
    {
        ProgramParameters Extract(params string[] args);
    }
}
