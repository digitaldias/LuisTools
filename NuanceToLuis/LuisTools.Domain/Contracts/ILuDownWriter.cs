﻿using System.Collections.Generic;

namespace LuisTools.Domain.Contracts
{
    public interface ILuDownWriter
    {
        int Write(string outputFolder, string originalFileName, List<string> linesToWrite);
    }
}
