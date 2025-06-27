using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class CharacterShotAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private GameObject _firePoint;
        [SerializeField] private float _projectileScale = 0.7f;
        [SerializeField] private float _fireRate = 3f;
        
        public class Baker : Baker<CharacterShotAuthoring>
        {
            public override void Bake(CharacterShotAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new CharacterShotData()
                {
                    ProjectilePrefab = GetEntity(authoring._projectilePrefab, TransformUsageFlags.Dynamic),
                    FireRate = authoring._fireRate,
                    FirePoint = GetEntity(authoring._firePoint, TransformUsageFlags.Dynamic),
                    ProjectileScale = authoring._projectileScale,
                    LastShotTime = 0,
                });
            }
        }
    }
}