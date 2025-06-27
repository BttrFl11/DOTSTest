using _Scripts.Components;
using Unity.Collections;
using Unity.Entities;

namespace _Scripts.Systems
{
    public partial struct CharacterHealthSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((
                         RefRO<CharacterHealthData> characterHealth,
                         Unity.Entities.Entity entity) in SystemAPI.Query<RefRO<CharacterHealthData>>().WithEntityAccess())
            {
                if (characterHealth.ValueRO.Health <= 0)
                {
                    buffer.DestroyEntity(entity);
                }
            }

            buffer.Playback(state.EntityManager);
        }
    }
}