using _Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace _Scripts.Systems
{
    public partial struct PlayerSpawnerSystem : ISystem
    {
        private EntityQuery _query;
        
        public void OnCreate(ref SystemState state)
        {
            _query = state.GetEntityQuery(typeof(PlayerSpawnerData));
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var playerSpawner in SystemAPI.Query<RefRW<PlayerSpawnerData>>())
            {
                var playerEntity = state.EntityManager.Instantiate(playerSpawner.ValueRO.Prefab);
                
                state.EntityManager.SetComponentData(playerEntity, LocalTransform.FromPosition(playerSpawner.ValueRO.Position));
            }

            state.EntityManager.RemoveComponent<PlayerSpawnerData>(_query);
        }
    }
}