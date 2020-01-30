using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text currentScoreLabel;
    public Text highscoreLabel;
    public GameObject gameOverLayer;
    public GameObject gameOverPanel;
    public Text gameOverScoreLabel;
    public LifeUIController lifeUIController;
    public Player player;

    private int score;
    private int lives;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        score = 0;
        lives = 3;

        highscoreLabel.text = SaveGame.GetHighscore().ToString();
    }

    public void AddScore(int amount)
    {
        score += amount;
        currentScoreLabel.text = score.ToString();
    }

    public void RemoveLife()
    {
        lifeUIController.UpdateUI(--lives);

        if (lives <= 0)
        {
            if (score > SaveGame.GetHighscore())
            {
                SaveGame.SetHighscore(score);
            }

            player.PlayerDeath();
            GetComponent<AudioSource>().DOFade(0, 0.4f);

            gameOverLayer.SetActive(true);
            gameOverLayer.GetComponent<Image>().DOColor(new Color32(0, 0, 0, 100), 1f);
            StartCoroutine(SlowDown());
        }
    }

    public IEnumerator SlowDown()
    {
        float increment = 0;
        while (increment < 0.5f)
        {
            increment += Time.deltaTime;
            Time.timeScale = Mathf.Lerp(0.5f, 0.1f, increment);
            yield return null;
        }

        Time.timeScale = 0;
        gameOverScoreLabel.text = score.ToString();
        gameOverPanel.SetActive(true);
    }

    public void Reload()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
