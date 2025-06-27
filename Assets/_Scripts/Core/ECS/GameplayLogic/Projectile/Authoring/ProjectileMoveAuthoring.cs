using _Scripts.Core.Entity.GameplayLogic.Projectile.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Core.Entity.GameplayLogic.Projectile.Authoring
{
    public class ProjectileMoveAuthoring : MonoBehaviour
    {
        [SerializeField] private float2 _positionSquareLimit = new float2(23, 15);
        [SerializeField] private float _moveSpeed = 15f;

        public class Baker : Baker<ProjectileMoveAuthoring>
        {
            public override void Bake(ProjectileMoveAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new ProjectileMoveData()
                {
                    MoveSpeed = authoring._moveSpeed,
                    Entity = entity,
                    PositionSquareLimit = authoring._positionSquareLimit,
                });
            }
        }
    }
}