using _Scripts.Core.Entity.GameplayLogic.Projectile.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Systems
{
    public partial struct ProjectileMoveSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);

            foreach ((
                         RefRO<ProjectileMoveData> projectileMove,
                         RefRW<LocalTransform> transform) 
                     in SystemAPI.Query<RefRO<ProjectileMoveData>, RefRW<LocalTransform>>())
            {
                transform.ValueRW.Position += SystemAPI.Time.DeltaTime * projectileMove.ValueRO.MoveSpeed * transform.ValueRW.Forward();

                var position = transform.ValueRO.Position;

                var limit = projectileMove.ValueRO.PositionSquareLimit;
                if (position.x > limit.x || position.x < -limit.x)
                {
                    buffer.DestroyEntity(projectileMove.ValueRO.Entity);
                }
                else if (position.z > limit.y || position.z < -limit.y)
                {
                    buffer.DestroyEntity(projectileMove.ValueRO.Entity);
                }
            }
            
            buffer.Playback(state.EntityManager);
        }
    }
}