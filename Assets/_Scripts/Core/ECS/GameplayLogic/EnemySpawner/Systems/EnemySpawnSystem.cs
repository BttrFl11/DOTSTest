using _Scripts.Core.EnemySpawner.Components;
using _Scripts.Core.Entity.Enemy.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Core.ECS.EnemySpawner.Systems
{
    public partial struct EnemySpawnSystem : ISystem 
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var enemySpawn in SystemAPI.Query<RefRW<EnemySpawnData>>())
            {
                if (SystemAPI.Time.ElapsedTime >= enemySpawn.ValueRO.LastSpawnTime + enemySpawn.ValueRO.SpawnRate)
                {
                    enemySpawn.ValueRW.LastSpawnTime = SystemAPI.Time.ElapsedTime;

                    var prefab = enemySpawn.ValueRO.EnemyPrefab;
                    var enemyEntity = state.EntityManager.Instantiate(prefab);
                    
                    state.EntityManager.SetComponentData(enemyEntity, 
                        LocalTransform.FromPosition(enemySpawn.ValueRO.SpawnPosition));
                    
                    state.EntityManager.SetComponentData(enemyEntity, new EnemyStartMovePointData()
                    {
                        Position = enemySpawn.ValueRO.StartMovePosition,
                        Set = true,
                    });
                }
            }
        }
    }
}