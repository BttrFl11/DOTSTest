using _Scripts.Components;
using _Scripts.Core.ECS.GameplayLogic.Projectile;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Systems
{
    public partial struct CharacterShotSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var characterShot in SystemAPI.Query<RefRW<CharacterShotData>>())
            {
                if (SystemAPI.Time.ElapsedTime >= characterShot.ValueRO.LastShotTime + 1 / characterShot.ValueRO.FireRate)
                {
                    characterShot.ValueRW.LastShotTime = SystemAPI.Time.ElapsedTime;

                    var projectileEntity = state.EntityManager.Instantiate(characterShot.ValueRO.ProjectilePrefab);
                    state.EntityManager.SetComponentData(projectileEntity, new LocalTransform()
                    {
                        Position = state.EntityManager.GetComponentData<LocalToWorld>(characterShot.ValueRO.FirePoint).Position,
                        Rotation = !math.all(characterShot.ValueRO.Direction == float3.zero) ?
                            Quaternion.LookRotation(characterShot.ValueRO.Direction) : 
                            quaternion.identity,
                        Scale = characterShot.ValueRO.ProjectileScale,
                    });
                    
                    buffer.AddComponent(projectileEntity, new CharacterProjectileTag());
                }
            }
            
            buffer.Playback(state.EntityManager);
        }
    }
}