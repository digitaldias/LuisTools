using System.Collections.Generic;

namespace DigitalDias.Domain.Contracts
{
    public interface IFileFinder
    {
        IEnumerable<string> Find(string pattern);

        string GetFolderName(string outputFolderName);
    }
}