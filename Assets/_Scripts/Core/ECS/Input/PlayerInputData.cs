using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.Components
{
    public struct PlayerInputData : IComponentData
    {
        public float3 MoveInput;
        public float3 MousePosition;
    }
}