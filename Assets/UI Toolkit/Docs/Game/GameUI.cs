using System;
using _Scripts.Components;
using _Scripts.Core.ECS.Player.Components;
using UI_Toolkit.Docs.Game.ECS.Tags;
using Unity.Collections;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI_Toolkit.Docs.Game
{
    public class GameUI : MonoBehaviour
    {
        private UIDocument _doc;

        private Entity _entity;
        private EntityManager _entityManager;

        private VisualElement _menuPanel;
        private ProgressBar _characterHealth;
        private EntityQuery _healthQuery;

        private void Awake()
        {
            _doc = GetComponent<UIDocument>();

            _menuPanel = _doc.rootVisualElement.Q<VisualElement>("ButtonsMenu");

            _characterHealth = _doc.rootVisualElement.Q<ProgressBar>("CharacterHealth");
            
            var menuBtn = _doc.rootVisualElement.Q<Button>("Menu");
            menuBtn.clicked += OpenMenu;
            
            var restartBtn = _doc.rootVisualElement.Q<Button>("Restart");
            restartBtn.clicked += Restart;
            
            var exitBtn = _doc.rootVisualElement.Q<Button>("Exit");
            exitBtn.clicked += Exit;
            

            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _entity = _entityManager.CreateEntity();
            _entityManager.AddComponent<PlayerUIData>(_entity);
            
            _healthQuery = _entityManager.CreateEntityQuery(typeof(CharacterHealthData));
        }

        private void Update()
        {
            if (_entityManager.HasComponent<PlayerUIData>(_entity))
            {
                var uiData = _entityManager.GetComponentData<PlayerUIData>(_entity);

                _menuPanel.visible = uiData.MenuPanelState;
            }

            foreach (var healthEntity in _healthQuery.ToEntityArray(AllocatorManager.Temp))
            {
                var healthData = _entityManager.GetComponentData<CharacterHealthData>(healthEntity);

                _characterHealth.value = healthData.Health / (float)healthData.MaxHealth;
            }
        }

        private void OpenMenu()
        {
            var entity = _entityManager.CreateEntity();
            _entityManager.AddComponent<OpenMenuGameUI>(entity);
        }

        private void Restart()
        {
            var entity = _entityManager.CreateEntity();
            _entityManager.AddComponent<RestartGameUI>(entity);
        }

        private void Exit()
        {
            var entity = _entityManager.CreateEntity();
            _entityManager.AddComponent<ExitGameUI>(entity);
        }
    }
}