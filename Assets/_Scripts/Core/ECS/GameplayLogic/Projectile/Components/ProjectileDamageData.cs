using Unity.Entities;
using UnityEngine.Serialization;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Components
{
    public struct ProjectileDamageData : IComponentData
    { 
        public float HitDistance;
        public int Damage;
    }
}