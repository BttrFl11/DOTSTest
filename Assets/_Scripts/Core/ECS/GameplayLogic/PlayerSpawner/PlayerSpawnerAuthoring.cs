using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class PlayerSpawnerAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _spawnPoint;
        
        public class Baker : Baker<PlayerSpawnerAuthoring>
        {
            public override void Bake(PlayerSpawnerAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new PlayerSpawnerData()
                {
                    Prefab = GetEntity(authoring._prefab, TransformUsageFlags.Dynamic),
                    Position = authoring._spawnPoint.position,
                });
            }
        }
    }
}