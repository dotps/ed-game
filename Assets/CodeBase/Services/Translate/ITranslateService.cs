using System.Threading.Tasks;

namespace CodeBase.Services.Translate
{
    public interface ITranslateService : IService
    {
        Task<TResultType> Get<TResultType>(string url);
    }
}