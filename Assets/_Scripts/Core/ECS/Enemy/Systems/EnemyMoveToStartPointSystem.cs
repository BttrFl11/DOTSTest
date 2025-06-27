using System;
using _Scripts.Core.Entity.Enemy.Authoring;
using _Scripts.Core.Entity.Enemy.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Systems
{
    public partial struct EnemyMoveToStartPointSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach ((
                         RefRW<EnemyMoveData> moveData,
                         RefRO<EnemyStartMovePointData> startMovePoint,
                         RefRW<LocalTransform> transform) in
                     SystemAPI.Query<RefRW<EnemyMoveData>, RefRO<EnemyStartMovePointData>, RefRW<LocalTransform>>())
            {
                if (moveData.ValueRO.State != EnemyMoveAuthoring.State.MoveToStartPoint)
                {
                    continue;
                }

                if (startMovePoint.ValueRO.Set == false)
                {
                    continue;
                }

                if (math.length(transform.ValueRO.Position - startMovePoint.ValueRO.Position) < moveData.ValueRO.DistanceToPointError)
                {
                    // better make new system for handling state changes?
                    moveData.ValueRW.State = EnemyMoveAuthoring.State.MoveToCharacter;

                    continue;
                }

                var direction = math.normalizesafe(startMovePoint.ValueRO.Position - transform.ValueRO.Position);
                transform.ValueRW.Position += moveData.ValueRO.Speed * SystemAPI.Time.DeltaTime * direction;
            }
        }
    }
}