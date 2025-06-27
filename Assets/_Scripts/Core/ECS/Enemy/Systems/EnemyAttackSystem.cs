using _Scripts.Components;
using _Scripts.Core.ECS.GameplayLogic.Projectile;
using _Scripts.Core.Entity.Enemy.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Systems
{
    public partial struct EnemyAttackSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach ((
                         RefRW<EnemyAttackData> enemyAttack,
                         RefRO<LocalTransform> enemyTransform,
                         Unity.Entities.Entity enemyEntity)
                     in SystemAPI.Query<RefRW<EnemyAttackData>, RefRO<LocalTransform>>().WithEntityAccess())
            {
                foreach ((
                             RefRW<CharacterHealthData> characterHealth,
                             RefRO<LocalTransform> characterTransform)
                         in SystemAPI.Query<RefRW<CharacterHealthData>, RefRO<LocalTransform>>())
                {
                    HandleFire(ref state,  ref buffer, enemyAttack, enemyTransform, characterHealth, characterTransform);
                    HandleMelee(ref state,  ref buffer, enemyAttack, enemyTransform, characterHealth, characterTransform, enemyEntity);

                    break;
                }
            }
            
            buffer.Playback(state.EntityManager);
        }

        private void HandleMelee(ref SystemState state,
            ref EntityCommandBuffer buffer,
            RefRW<EnemyAttackData> enemyAttack,
            RefRO<LocalTransform> enemyTransform,
            RefRW<CharacterHealthData> characterHealth,
            RefRO<LocalTransform> characterTransform, 
            Unity.Entities.Entity enemyEntity)
        {
            var distance = math.length(characterTransform.ValueRO.Position - enemyTransform.ValueRO.Position);

            if (distance <= enemyAttack.ValueRO.MeleeAttackDistance)
            {
                buffer.DestroyEntity(enemyEntity);

                characterHealth.ValueRW.Health -= enemyAttack.ValueRO.Damage * 2;
            }
        }

        private void HandleFire(ref SystemState state, 
            ref EntityCommandBuffer buffer, 
            RefRW<EnemyAttackData> enemyAttack,
            RefRO<LocalTransform> enemyTransform,
            RefRW<CharacterHealthData> characterHealth,
            RefRO<LocalTransform> characterTransform)
        {
            if (SystemAPI.Time.ElapsedTime >= enemyAttack.ValueRO.LastFireTime + 1 / enemyAttack.ValueRO.FireRate)
            {
                enemyAttack.ValueRW.LastFireTime = SystemAPI.Time.ElapsedTime;

                var direction = math.normalizesafe(characterTransform.ValueRO.Position - enemyTransform.ValueRO.Position);
                
                var projectileEntity = state.EntityManager.Instantiate(enemyAttack.ValueRO.ProjectilePrefab);
                
                state.EntityManager.SetComponentData(projectileEntity, new LocalTransform()
                {
                    Position = state.EntityManager.GetComponentData<LocalToWorld>(enemyAttack.ValueRO.FirePoint).Position,
                    Rotation = Quaternion.LookRotation(direction),
                    Scale = enemyAttack.ValueRO.ProjectileScale,
                });
                    
                buffer.AddComponent(projectileEntity, new EnemyProjectileTag());
            }
        }
    }
}