using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
using Unity.Mathematics;

public struct BirdSpawner : IComponentData
{
    public Entity BirdPrefab;
    public int BirdsPerSecond;
}

public class SpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    [SerializeField]
    GameObject _birdPrefab;

    [SerializeField]
    int _birdsPerSecond;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BirdSpawner
        {
            BirdPrefab = conversionSystem.GetPrimaryEntity(_birdPrefab),
            BirdsPerSecond = _birdsPerSecond,
        });
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(_birdPrefab);
    }
}
