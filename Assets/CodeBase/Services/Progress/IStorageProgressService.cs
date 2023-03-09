using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public interface IStorageProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}