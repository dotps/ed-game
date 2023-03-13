using System.Collections.Generic;
using CodeBase.Data.Words;

namespace CodeBase.Services.Words
{
    public interface IWordService : IService
    {
        Dictionary<string, WordData> Load();
        void Save(string data);
    }
}