using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace _Scripts.Components
{
    public struct CharacterMoveData : IComponentData
    {
        public float2 PositionSquareLimit;
        public float Speed;
    }
}