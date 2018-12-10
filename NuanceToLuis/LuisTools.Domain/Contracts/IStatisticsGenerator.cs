using LuisTools.Domain.Entities;

namespace LuisTools.Domain.Contracts
{
    public interface IStatisticsGenerator
    {
        string Generate(FileStatistic statistic);
    }
}
