using _Scripts.Core.Entity.Enemy.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Authoring
{
    public class EnemyHealthAuthoring : MonoBehaviour
    {
        [SerializeField] private int _health = 1;
        
        public class Baker : Baker<EnemyHealthAuthoring>
        {
            public override void Bake(EnemyHealthAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyHealthData()
                {
                    Health = authoring._health,
                });
            }
        }
    }
}