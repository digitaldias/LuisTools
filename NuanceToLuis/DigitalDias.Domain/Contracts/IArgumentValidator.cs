namespace DigitalDias.Domain.Contracts
{
    public interface IArgumentValidator
    {
        bool IsValid(string[] args);
    }
}
