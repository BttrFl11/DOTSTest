using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.Core.Entity.Enemy.Components
{
    public struct EnemyStartMovePointData : IComponentData
    {
        public bool Set;
        public float3 Position;
    }
}