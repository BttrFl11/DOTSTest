using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class CharacterHealthAuthoring : MonoBehaviour
    {
        [SerializeField] private int _health = 11;
        
        public class Baker : Baker<CharacterHealthAuthoring>
        {
            public override void Bake(CharacterHealthAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterHealthData()
                {
                    Health = authoring._health,
                    MaxHealth = authoring._health,
                });
            }
        }
    }
}