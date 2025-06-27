using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.Components
{
    public struct CharacterShotData : IComponentData
    {
        public Entity ProjectilePrefab;
        public float ProjectileScale;
        public Entity FirePoint;
        public float FireRate;

        public float3 Direction;
        public double LastShotTime;
    }
}