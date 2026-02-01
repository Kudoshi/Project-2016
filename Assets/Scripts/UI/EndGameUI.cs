using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class EndGameUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text totalTimeText;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private GameObject newBestLabel;
        [SerializeField] private string mainMenuSceneName = "MainMenu";

        private const string BestScoreKey = "BestScore";

        public void Show(int score, float totalTime)
        {
            if (finalScoreText != null)
                finalScoreText.text = score.ToString();

            if (totalTimeText != null)
            {
                int minutes = Mathf.FloorToInt(totalTime / 60f);
                int seconds = Mathf.FloorToInt(totalTime % 60f);
                totalTimeText.text = $"{minutes:00}:{seconds:00}";
            }

            int bestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            bool isNewBest = score > bestScore;

            if (isNewBest)
            {
                bestScore = score;
                PlayerPrefs.SetInt(BestScoreKey, bestScore);
                PlayerPrefs.Save();
            }

            if (highScoreText != null)
                highScoreText.text = bestScore.ToString();

            if (newBestLabel != null)
                newBestLabel.SetActive(isNewBest);

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
