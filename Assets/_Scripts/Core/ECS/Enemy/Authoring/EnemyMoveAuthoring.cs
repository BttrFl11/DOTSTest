using _Scripts.Core.Entity.Enemy.Components;
using Unity.Entities;
using UnityEngine;

namespace _Scripts.Core.Entity.Enemy.Authoring
{
    public class EnemyMoveAuthoring : MonoBehaviour
    {
        [SerializeField] private float _speed = 7;
        [SerializeField] private float _distanceToPointError = 0.5f;
        [SerializeField] private State _state = State.MoveToStartPoint;
        
        public enum State
        {
            None,
            MoveToStartPoint,
            MoveToCharacter,
        }
        
        public class Baker : Baker<EnemyMoveAuthoring>
        {
            public override void Bake(EnemyMoveAuthoring authoring)
            {
                var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyMoveData()
                {
                    Speed = authoring._speed,
                    DistanceToPointError = authoring._distanceToPointError,
                    State = authoring._state,
                });
            }
        }
    }
}