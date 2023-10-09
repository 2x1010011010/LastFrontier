using System.Collections;
using System.Collections.Generic;
using CodeBase.Generators.MapGenerator;
using UnityEngine;

public class GameEventHAndler : MonoBehaviour
{
    [SerializeField] private MapSpawner _spawner;
    void Start()
    {
        _spawner.SpawnMap();
    }
}
