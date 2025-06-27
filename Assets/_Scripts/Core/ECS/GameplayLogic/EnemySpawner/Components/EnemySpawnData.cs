using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.Core.EnemySpawner.Components
{
    public struct EnemySpawnData : IComponentData
    {
        public Unity.Entities.Entity EnemyPrefab;
        public float3 SpawnPosition;
        public float3 StartMovePosition;
        public float SpawnRate;
        
        public double LastSpawnTime;
    }
}