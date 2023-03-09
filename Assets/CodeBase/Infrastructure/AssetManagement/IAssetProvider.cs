using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject LoadAndInstantiate(string path);
        GameObject LoadAndInstantiate(string path, Vector3 position);
    }
}