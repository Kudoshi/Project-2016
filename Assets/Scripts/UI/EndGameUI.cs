using TMPro;
using UnityEngine;

namespace UI
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        public void Show(int score)
        {
            if (finalScoreText != null)
                finalScoreText.text = score.ToString();

            gameObject.SetActive(true);
        }

        public void OnRetry()
        {
            SceneLoader.ReloadCurrentScene();
        }

        public void OnBackToMenu()
        {
            SceneLoader.LoadScene(mainMenuSceneName);
        }
    }
}
