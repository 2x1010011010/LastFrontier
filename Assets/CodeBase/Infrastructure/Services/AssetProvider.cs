using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string path) =>
      Object.Instantiate(GetObjectByPath(path));
    

    public GameObject Instantiate(string path, Vector3 at) =>
      Object.Instantiate(GetObjectByPath(path), at, Quaternion.identity);

    private GameObject GetObjectByPath(string path) =>
      Resources.Load<GameObject>(path);    
  }
}