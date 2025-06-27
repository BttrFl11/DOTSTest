using System;
using _Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Systems
{
    public partial struct CharacterShotDirectionSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var playerInput in SystemAPI.Query<RefRO<PlayerInputData>>())
            {
                foreach ((
                             RefRO<CharacterTargetData> characterTarget,
                             RefRO<LocalToWorld> transform)
                         in SystemAPI.Query<RefRO<CharacterTargetData>, RefRO<LocalToWorld>>())
                {
                    foreach (var characterShot in SystemAPI.Query<RefRW<CharacterShotData>>())
                    {
                        var direction = playerInput.ValueRO.MousePosition - transform.ValueRO.Position;
                        direction.y = 0;

                        if (!math.all(direction.xyz == float3.zero))
                        {
                            direction = math.normalize(direction);
                        }

                        characterShot.ValueRW.Direction = direction;
                    }
                }
            }
        }
    }
}