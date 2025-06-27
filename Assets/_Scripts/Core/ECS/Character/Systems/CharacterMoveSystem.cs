using _Scripts.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace _Scripts.Systems
{
    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (RefRO<PlayerInputData> playerInputData in SystemAPI.Query<RefRO<PlayerInputData>>())
            {
                foreach ((
                             RefRW<LocalTransform> localTransform,
                             RefRO<CharacterMoveData> characterMove)
                         in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CharacterMoveData>>())
                {
                    var position = localTransform.ValueRO.Position;
                    position += SystemAPI.Time.DeltaTime *
                                characterMove.ValueRO.Speed *
                                playerInputData.ValueRO.MoveInput;

                    position.x = math.clamp(position.x, 
                        -characterMove.ValueRO.PositionSquareLimit.x, 
                        characterMove.ValueRO.PositionSquareLimit.x);

                    position.z = math.clamp(position.z, 
                        -characterMove.ValueRO.PositionSquareLimit.y, 
                        characterMove.ValueRO.PositionSquareLimit.y);

                    localTransform.ValueRW.Position = position;
                }

                break;
            }
        }
    }
}