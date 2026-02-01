using UnityEditor;
using UnityEngine;

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
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (modeSelectionPanel != null) modeSelectionPanel.SetActive(false);
            if (creditsPanel != null) creditsPanel.SetActive(false);

            if (panel != null) panel.SetActive(true);
        }

        public void OnStartButton()
        {
            if (!string.IsNullOrEmpty(gameSceneName))
                SceneLoader.LoadScene(gameSceneName);
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
                SceneLoader.LoadScene(gameSceneName);
            else
                Debug.LogWarning("[MenuManager] Game scene name is not set.");
        }
    }
}