using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Words;

namespace CodeBase.Services.Words
{
    public interface IWordService : IService
    {
        bool TryAdd(WordData word);
        void SubscribeUpdates();
        void UnSubscribeUpdates();
    }
}