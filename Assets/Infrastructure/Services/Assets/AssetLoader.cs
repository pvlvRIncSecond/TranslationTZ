using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetLoader : IAssetLoader
    {
        public GameObject Instantiate(string assetPath, Transform parent) =>
            Object.Instantiate(Load(assetPath), parent);

        public GameObject Instantiate(string assetPath) =>
            Object.Instantiate(Load(assetPath));

        private GameObject Load(string assetPath) =>
            Resources.Load<GameObject>(assetPath);
    }
}