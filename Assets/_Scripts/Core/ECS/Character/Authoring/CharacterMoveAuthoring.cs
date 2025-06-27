using _Scripts.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts
{
    public class CharacterMoveAuthoring : MonoBehaviour
    {
        [SerializeField] private Vector2 _positionSquareLimit = new Vector2(23, 15);
        [SerializeField] private float _moveSpeed = 10;
        
        public class Baker : Baker<CharacterMoveAuthoring>
        {
            public override void Bake(CharacterMoveAuthoring moveAuthoring)
            {
                var entity = GetEntity(moveAuthoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new CharacterMoveData()
                {
                    PositionSquareLimit = moveAuthoring._positionSquareLimit,
                    Speed = moveAuthoring._moveSpeed,
                });
            }
        }
    }
}