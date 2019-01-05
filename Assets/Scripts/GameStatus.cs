using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // configuration parameters
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 83;
    [SerializeField] private TextMeshProUGUI scoreText;

    // state variables
    [SerializeField] private int currentScore = 0;

    public void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        scoreText.text = currentScore.ToString();
        currentScore += pointsPerBlockDestroyed;
    }
}