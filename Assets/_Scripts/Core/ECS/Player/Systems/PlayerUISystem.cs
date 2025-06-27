using _Scripts.Core.ECS.Player.Components;
using UI_Toolkit.Docs.Game.ECS.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Core.ECS.Player.Systems
{
    public partial struct PlayerUISystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var buffer = new EntityCommandBuffer(Allocator.Temp);

            HandleOpenMenu(ref state, ref buffer);
            HandleExit(ref state, ref buffer);
            HandleRestart(ref state, ref buffer);

            buffer.Playback(state.EntityManager);
        }

        private void HandleOpenMenu(ref SystemState state, ref EntityCommandBuffer buffer)
        {
            foreach ((
                         _,
                         Unity.Entities.Entity entity) 
                     in SystemAPI.Query<OpenMenuGameUI>().WithEntityAccess())
            {
                foreach (var playerUI in SystemAPI.Query<RefRW<PlayerUIData>>())
                {
                    playerUI.ValueRW.MenuPanelState = !playerUI.ValueRW.MenuPanelState;
                }
                
                buffer.DestroyEntity(entity);
            }
        }
        
        private void HandleExit(ref SystemState state, ref EntityCommandBuffer buffer)
        {
            foreach ((
                         _,
                         Unity.Entities.Entity entity) 
                     in SystemAPI.Query<ExitGameUI>().WithEntityAccess())
            {
                buffer.DestroyEntity(entity);

                SceneManager.LoadScene("Menu");
            }
        }
        
        private void HandleRestart(ref SystemState state, ref EntityCommandBuffer buffer)
        {
            foreach ((
                         _,
                         Unity.Entities.Entity entity) 
                     in SystemAPI.Query<RestartGameUI>().WithEntityAccess())
            {
                buffer.DestroyEntity(entity);

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}