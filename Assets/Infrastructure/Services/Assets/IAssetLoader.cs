using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetLoader : IService
    {
        GameObject Instantiate(string assetPath);
        GameObject Instantiate(string assetPath, Transform parent);
    }
}