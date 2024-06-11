using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
  public interface IAssetProvider : IService
  {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 at);
  }
}