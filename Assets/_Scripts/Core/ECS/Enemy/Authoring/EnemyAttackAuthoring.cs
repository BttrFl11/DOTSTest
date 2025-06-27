using _Scripts.Core.Entity.Enemy.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Authoring
{
    public class EnemyAttackAuthoring : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _fireRate = 0.3f;
        [SerializeField] private GameObject _firePoint;
        [SerializeField] private float _projectileScale = 0.7f;
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private float _meleeAttackDistance = 3f; //kamikadzeDistance :3
        
        public class Baker : Baker<EnemyAttackAuthoring>
        {
            public override void Bake(EnemyAttackAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyAttackData()
                {
                    Damage = authoring._damage,
                    FirePoint = GetEntity(authoring._firePoint, TransformUsageFlags.Dynamic),
                    FireRate = authoring._fireRate,
                    LastFireTime = 0,
                    ProjectilePrefab = GetEntity(authoring._projectilePrefab, TransformUsageFlags.Dynamic),
                    ProjectileScale = authoring._projectileScale,
                    MeleeAttackDistance = authoring._meleeAttackDistance,
                });
            }
        }
    }
}