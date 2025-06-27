using _Scripts.Components;
using _Scripts.Core.ECS.GameplayLogic.Projectile;
using _Scripts.Core.Entity.Enemy.Components;
using _Scripts.Core.Entity.GameplayLogic.Projectile.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Systems
{
    public partial struct ProjectileDamageSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);

            //simple but not the best solution
            HandleCharacterProjectile(ref state, ref buffer);
            HandleEnemyProjectile(ref state, ref buffer);

            buffer.Playback(state.EntityManager);
        }

        private void HandleEnemyProjectile(ref SystemState state, ref EntityCommandBuffer buffer)
        {
            foreach ((
                         RefRO<ProjectileDamageData> projectileDamage,
                         RefRO<LocalTransform> projectileTransform,
                         RefRO<EnemyProjectileTag> enemyProjectileTag,
                         Unity.Entities.Entity projectileEntity)
                     in SystemAPI.Query<RefRO<ProjectileDamageData>, RefRO<LocalTransform>, RefRO<EnemyProjectileTag>>().WithEntityAccess())
            {
                foreach ((
                             RefRW<CharacterHealthData> characterHealth,
                             RefRO<LocalTransform> characterTransform,
                             Unity.Entities.Entity characterEntity)
                         in SystemAPI.Query<RefRW<CharacterHealthData>, RefRO<LocalTransform>>().WithEntityAccess())
                {
                    var distance = math.length(projectileTransform.ValueRO.Position - characterTransform.ValueRO.Position);
                    if (distance <= projectileDamage.ValueRO.HitDistance)
                    {
                        buffer.DestroyEntity(projectileEntity);

                        characterHealth.ValueRW.Health -= projectileDamage.ValueRO.Damage;

                        break;
                    }
                }
            }
        }

        private void HandleCharacterProjectile(ref SystemState state, ref EntityCommandBuffer buffer)
        {
            foreach ((
                         RefRO<ProjectileDamageData> projectileDamage,
                         RefRO<LocalTransform> projectileTransform,
                         RefRO<CharacterProjectileTag> characterProjectileTag,
                         Unity.Entities.Entity projectileEntity)
                     in SystemAPI.Query<RefRO<ProjectileDamageData>, RefRO<LocalTransform>, RefRO<CharacterProjectileTag>>().WithEntityAccess())
            {
                foreach ((
                             RefRW<EnemyHealthData> enemyHealth,
                             RefRO<LocalTransform> enemyTransform,
                             Unity.Entities.Entity enemyEntity)
                         in SystemAPI.Query<RefRW<EnemyHealthData>, RefRO<LocalTransform>>().WithEntityAccess())
                {
                    var distance = math.length(projectileTransform.ValueRO.Position - enemyTransform.ValueRO.Position);
                    if (distance <= projectileDamage.ValueRO.HitDistance)
                    {
                        buffer.DestroyEntity(projectileEntity);

                        enemyHealth.ValueRW.Health -= projectileDamage.ValueRO.Damage;

                        break;
                    }
                }
            }
        }
    }
}