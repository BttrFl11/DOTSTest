using Unity.Entities;

namespace _Scripts.Components
{
    public struct CharacterHealthData : IComponentData
    {
        public int MaxHealth;
        public int Health;
    }
}