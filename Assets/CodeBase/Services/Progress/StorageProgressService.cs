using CodeBase.Data;

namespace CodeBase.Services.Progress
{
    public class StorageProgressService : IStorageProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}