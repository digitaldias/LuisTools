using DigitalDias.Domain.Entities;

namespace DigitalDias.Domain.Contracts
{
    public interface IStatisticsGenerator
    {
        string Generate(FileStatistic statistic);
    }
}
