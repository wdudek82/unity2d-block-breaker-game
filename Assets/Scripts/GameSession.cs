using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // configuration parameters
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 83;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isAutoplay;

    // state variables
    [SerializeField] private int currentScore;
    
    private void Awake()
    {
        GameObject gameObjectCached = gameObject;
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObjectCached.SetActive(false);
            Destroy(gameObjectCached);
        }
        else
        {
            DontDestroyOnLoad(gameObjectCached);
        }
    }

    public void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

        ChangeGameSpeed();
        ToggleAutoPlay();
//        PauseGame();
    }
    
    private void ToggleAutoPlay()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SetIsAutoplayEnabled(!isAutoplay);
        }
    }

    private void ChangeGameSpeed()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            float value = gameSpeed > 0f ? 0.5f : 0;
            SetGameSpeed(gameSpeed - value);
        }
        else if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            float value = gameSpeed < 10f ? 0.5f : 0;
            SetGameSpeed(gameSpeed + value);
        }
    }

    public void SetGameSpeed(float value)
    {
        gameSpeed = value;
    }

    public bool IsAutoplayEnabled()
    {
        return isAutoplay;
    }

    public void SetIsAutoplayEnabled(bool value)
    {
        isAutoplay = value;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        Destroy(gameObject);
    }
}