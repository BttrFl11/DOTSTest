using _Scripts.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts
{
    public class PlayerInputAuthoring : MonoBehaviour
    {
        public class Baker : Baker<PlayerInputAuthoring>
        {
            public override void Bake(PlayerInputAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.None);
                AddComponent(entity, new PlayerInputData
                {
                    MoveInput = float3.zero,
                });
            }
        }
    }
}