using Unity.Entities;

namespace _Scripts.Core.Entity.Enemy.Components
{
    public struct EnemyAttackData : IComponentData
    {
        public int Damage;
        public Unity.Entities.Entity ProjectilePrefab;
        public float FireRate;
        public float ProjectileScale;
        public Unity.Entities.Entity FirePoint;
        public double LastFireTime;
        public float MeleeAttackDistance;
    }
}