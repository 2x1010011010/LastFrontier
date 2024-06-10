using System;
using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }

    public GameFactory(Func<IService> assetProvider)
    {
      throw new NotImplementedException();
    }

    public GameObject CreateMap()
    {
      throw new System.NotImplementedException();
    }

    public void CreateHud()
    {
      throw new System.NotImplementedException();
    }
  }
}