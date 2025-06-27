using _Scripts.Core.Entity.Enemy.Components;
using Unity.Collections;
using Unity.Entities;

namespace _Scripts.Core.Entity.Enemy.Systems
{
    public partial struct EnemyHealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((
                         RefRO<EnemyHealthData> enemyHealth,
                         Unity.Entities.Entity entity) in SystemAPI.Query<RefRO<EnemyHealthData>>().WithEntityAccess())
            {
                if (enemyHealth.ValueRO.Health <= 0)
                {
                    buffer.DestroyEntity(entity);
                }
            }

            buffer.Playback(state.EntityManager);
        }
    }
}