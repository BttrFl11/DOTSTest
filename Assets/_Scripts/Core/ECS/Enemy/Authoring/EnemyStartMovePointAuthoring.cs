using _Scripts.Core.Entity.Enemy.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Authoring
{
    public class EnemyStartMovePointAuthoring : MonoBehaviour
    {
        public class Baker : Baker<EnemyStartMovePointAuthoring>
        {
            public override void Bake(EnemyStartMovePointAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new EnemyStartMovePointData()
                {
                    Set = false,
                    Position = float3.zero,
                });
            }
        }
    }
}