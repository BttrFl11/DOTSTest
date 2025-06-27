using _Scripts.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Systems
{
    public partial struct PlayerInputControlSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (
                RefRW<PlayerInputData> playerInput
                in SystemAPI.Query<RefRW<PlayerInputData>>())
            {
                HandleMoveInput(playerInput);
                HandleMousePosition(ref state, playerInput);
            }
        }

        private void HandleMousePosition(ref SystemState state, RefRW<PlayerInputData> playerInput)
        {
            foreach (var gameplayCamera in SystemAPI.Query<RefRO<GameplayCameraData>>())
            {
                var entity = gameplayCamera.ValueRO.Camera;
                
                var camera = state.EntityManager.GetComponentObject<Camera>(entity);
                if (camera != null)
                {
                    var mouseCameraRay = camera.ScreenPointToRay(Input.mousePosition);

                    var plane = new Plane(Vector3.up, Vector3.zero);
                    if (plane.Raycast(mouseCameraRay, out float distance))
                    {
                        var point = mouseCameraRay.GetPoint(distance);
                        playerInput.ValueRW.MousePosition = point;
                    }

                    break;
                }    
            }
        }

        private void HandleMoveInput(RefRW<PlayerInputData> playerInput)
        {
            playerInput.ValueRW.MoveInput.xyz = float3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                playerInput.ValueRW.MoveInput.z = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerInput.ValueRW.MoveInput.z = -1;
            }

            if (Input.GetKey(KeyCode.D))
            {
                playerInput.ValueRW.MoveInput.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerInput.ValueRW.MoveInput.x = -1;
            }

            playerInput.ValueRW.MoveInput.xyz = math.normalizesafe(playerInput.ValueRO.MoveInput);
        }
    }
}