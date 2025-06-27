using _Scripts.Components;
using _Scripts.Core.Entity.Enemy.Authoring;
using _Scripts.Core.Entity.Enemy.Components;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Systems
{
    public partial struct EnemyMoveToCharacterSystem : ISystem
    {
        private EntityQuery _characterQuery;
        private NativeArray<Unity.Entities.Entity> _characterEntities;

        public void OnCreate(ref SystemState state)
        {
            _characterQuery = state.GetEntityQuery(typeof(CharacterTargetData));
        }

        public void OnUpdate(ref SystemState state)
        {
            foreach ((
                         RefRW<EnemyMoveData> enemyMove,
                         RefRW<LocalTransform> transform)
                     in SystemAPI.Query<RefRW<EnemyMoveData>, RefRW<LocalTransform>>())
            {
                foreach (var characterEntity in _characterQuery.ToEntityArray(AllocatorManager.Temp))
                {
                    if (enemyMove.ValueRO.State != EnemyMoveAuthoring.State.MoveToCharacter)
                    {
                        continue;
                    }

                    var characterTransform = state.EntityManager.GetComponentData<LocalTransform>(characterEntity);

                    var direction = math.normalizesafe(characterTransform.Position - transform.ValueRO.Position);

                    transform.ValueRW.Position += enemyMove.ValueRO.Speed * SystemAPI.Time.DeltaTime * direction;

                    break;
                }
            }
        }
    }
}