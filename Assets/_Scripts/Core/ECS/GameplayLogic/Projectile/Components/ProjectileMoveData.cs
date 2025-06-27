using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.Serialization;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Components
{
    public struct ProjectileMoveData : IComponentData
    {
        public int Damage;
        public float MoveSpeed;
        public float2 PositionSquareLimit;
        public Unity.Entities.Entity Entity;
    }
}