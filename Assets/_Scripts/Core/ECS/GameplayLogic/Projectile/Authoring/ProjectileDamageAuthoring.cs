using _Scripts.Core.Entity.GameplayLogic.Projectile.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Authoring
{
    public class ProjectileDamageAuthoring : MonoBehaviour
    {
        [SerializeField] private float _hitDistance = 0.25f;
        [SerializeField] private int _damage = 1;

        public class Baker : Baker<ProjectileDamageAuthoring>
        {
            public override void Bake(ProjectileDamageAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ProjectileDamageData()
                {
                    Damage = authoring._damage,
                    HitDistance = authoring._hitDistance,
                });
            }
        }
    }
}