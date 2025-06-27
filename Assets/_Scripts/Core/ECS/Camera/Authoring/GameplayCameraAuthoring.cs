using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class GameplayCameraAuthoring : MonoBehaviour
    {
        public class Baker : Baker<GameplayCameraAuthoring>
        {
            public override void Bake(GameplayCameraAuthoring authoring)
            {
                Entity entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new GameplayCameraData
                {
                    Camera = entity,
                });
            }
        }
    }
}