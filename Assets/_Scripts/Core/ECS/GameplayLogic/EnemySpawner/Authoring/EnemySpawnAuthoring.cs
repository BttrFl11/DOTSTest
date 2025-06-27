using _Scripts.Core.EnemySpawner.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.Entity.EnemySpawner.Authoring
{
    public class EnemySpawnAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private float _spawnRate = 1;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _startMovePoint;

        public class Baker : Baker<EnemySpawnAuthoring>
        {
            public override void Bake(EnemySpawnAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new EnemySpawnData()
                {
                    EnemyPrefab = GetEntity(authoring._prefab, TransformUsageFlags.Dynamic),
                    LastSpawnTime = 0,
                    SpawnPosition = authoring._spawnPoint.position,
                    SpawnRate = authoring._spawnRate,
                    StartMovePosition = authoring._startMovePoint.position,
                });
            }
        }
    }
}