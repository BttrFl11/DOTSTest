using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _Scripts.Menu.UI
{
    public class MenuUI : MonoBehaviour
    {
        private void Awake()
        {
            var doc = GetComponent<UIDocument>();

            var playBtn = doc.rootVisualElement.Q<Button>("Play");
            playBtn.clicked += LoadGame;
            
            var exitBtn = doc.rootVisualElement.Q<Button>("Exit");
            exitBtn.clicked += Quit;
        }

        private void Quit()
        {
            Application.Quit();
        }

        private void LoadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}