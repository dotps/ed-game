namespace CodeBase.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;
        public static ServiceLocator Instance => _instance ?? (_instance = new ServiceLocator());

        public void RegisterSingleInstance<TService>(TService implementation) where TService : IService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService GetSingleInstance<TService>() where TService : IService => 
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}