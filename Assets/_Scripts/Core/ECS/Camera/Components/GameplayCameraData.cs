using Unity.Entities;
using UnityEngine;

namespace _Scripts.Components
{
    public struct GameplayCameraData : IComponentData
    {
        public Entity Camera;
    }
}