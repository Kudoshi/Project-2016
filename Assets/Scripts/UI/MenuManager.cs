using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Panels")] [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject modeSelectionPanel;
        [SerializeField] private GameObject creditsPanel;

        [Header("Scene")] [SerializeField] private string gameSceneName;

        private void Start()
        {
            ShowPanel(mainMenuPanel);
        }

        private void ShowPanel(GameObject panel)
        {
            mainMenuPanel.SetActive(false);
            modeSelectionPanel.SetActive(false);
            creditsPanel.SetActive(false);

            panel.SetActive(true);
        }

        public void OnStartButton()
        {
            if (!string.IsNullOrEmpty(gameSceneName))
                SceneManager.LoadScene(gameSceneName);
            else
                Debug.LogWarning("[MenuManager] Game scene name is not set.");
        }

        public void OnCreditsButton()
        {
            ShowPanel(creditsPanel);
        }

        public void OnBackButton()
        {
            ShowPanel(mainMenuPanel);
        }

        public void OnQuitButton()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void OnSelectGameModeButton(int mode)
        {
            GameModeSelection.Current = (GameMode)mode;
            if (!string.IsNullOrEmpty(gameSceneName))
                SceneManager.LoadScene(gameSceneName);
            else
                Debug.LogWarning("[MenuManager] Game scene name is not set.");
        }
    }
}