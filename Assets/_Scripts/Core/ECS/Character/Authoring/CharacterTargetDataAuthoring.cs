using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class CharacterTargetDataAuthoring : MonoBehaviour
    {
        public class Baker : Baker<CharacterTargetDataAuthoring>
        {
            public override void Bake(CharacterTargetDataAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterTargetData());
            }
        }
    }
}