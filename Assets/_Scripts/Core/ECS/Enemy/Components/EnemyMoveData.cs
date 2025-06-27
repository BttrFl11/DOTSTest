using _Scripts.Core.Entity.Enemy.Authoring;
using Unity.Entities;

namespace _Scripts.Core.Entity.Enemy.Components
{
    public struct EnemyMoveData : IComponentData
    {
        public float Speed;
        public float DistanceToPointError;
        public EnemyMoveAuthoring.State State;
    }
}