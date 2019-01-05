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
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
    }
}