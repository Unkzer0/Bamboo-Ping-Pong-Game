using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button restartButton;
    [SerializeField] Text scoreLeftText;
    [SerializeField] Text scoreRightText;
    [SerializeField] Text timerText; 
    [SerializeField] GameObject ballPrefab;

    public int scoreLeft = 0, scoreRight = 0;
    private int maxScore = 11;
    private float timeRemaining = 300.0f;  
    private bool gameEnded = false;
    private GameObject currentBall;
    public AIPaddle leftPaddle;
    public PaddleController rightPaddle;

    void Awake()
    {
        Instance = this;   
    }

    void Start()
    {
        gameOverPanel.SetActive(false);

        if (GameSettings.Instance != null)
        {
            switch (GameSettings.Instance.SelectedMode)
            {
                case GameSettings.GameMode.Easy:
                    leftPaddle.reactionSpeed = 0.05f;
                    timeRemaining = 300f;
                    break;
                case GameSettings.GameMode.Hard:
                    leftPaddle.reactionSpeed = 0.5f;
                    timeRemaining = 300f;
                    break;
                case GameSettings.GameMode.Classic:
                    leftPaddle.reactionSpeed = 0.1f;
                    timeRemaining = Mathf.Infinity;
                    if (timerText != null)
                        timerText.gameObject.SetActive(false);
                    break;
            }
        }
        SpawnBall();
    }

    void Update()
    {
        if (!gameEnded && timeRemaining != Mathf.Infinity)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
{
    if (timeRemaining > 0)
    {
        timeRemaining -= Time.deltaTime;

        // Clamp to zero if it goes below
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame();
        }

        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = minutes.ToString("0") + ":" + seconds.ToString("00");
    }
}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ScorePoint(string player)
    {
        if (player == "Left")
        {
            scoreLeft++;
            rightPaddle.ShrinkPaddle();
            if (scoreLeftText != null) scoreLeftText.text = scoreLeft.ToString();
        }
        else
        {
            scoreRight++;
            leftPaddle.ShrinkPaddle();
            if (scoreRightText != null) scoreRightText.text = scoreRight.ToString();
        }

        if (scoreLeft >= maxScore || scoreRight >= maxScore)
        {
            EndGame();
            return;
        }

        StartCoroutine(ResetBall());
    }

    IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(1f);

        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        SpawnBall();
    }

    void SpawnBall()
    {
        if (gameEnded) return;
        currentBall = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity);
    }

    public void RespawnBall()
    {
        if (currentBall != null)
        {
            Destroy(currentBall);
        }

        StartCoroutine(ResetBall());
    }

    void EndGame()
    {
        if (gameEnded) return;

        gameEnded = true;  
        gameOverPanel.SetActive(true);
        restartButton.onClick.AddListener(RestartGame);
    }
}
