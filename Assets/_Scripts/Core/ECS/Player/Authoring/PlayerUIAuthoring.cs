using _Scripts.Core.ECS.Player.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.ECS.Player.Authoring
{
    public class PlayerUIAuthoring : MonoBehaviour
    {
        public class Baker : Baker<PlayerUIAuthoring>
        {
            public override void Bake(PlayerUIAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Renderable);
                AddComponent(entity, new PlayerUIData()
                {
                    MenuPanelState = false,
                });
            }
        }
    }
}