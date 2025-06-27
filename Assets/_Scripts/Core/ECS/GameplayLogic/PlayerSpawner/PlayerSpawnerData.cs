using Unity.Entities;
using Unity.Mathematics;

namespace _Scripts.Components
{
    public struct PlayerSpawnerData : IComponentData
    {
        public float3 Position;
        public Entity Prefab;
    }
}