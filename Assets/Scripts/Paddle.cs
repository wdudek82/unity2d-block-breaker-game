using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration parameteres
    [SerializeField] private float minX = 1f;
    [SerializeField] private float maxX = 15f;
    [SerializeField] private float screenWidthInUnits = 16f;
    
    // cached reference
    private GameSession gameSession;
    private Ball ball;
    
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        Vector2 paddlePos = new Vector2(position.x, position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoplayEnabled())
        {
            return ball.transform.position.x;
        }
        
        return Input.mousePosition.x / Screen.width * screenWidthInUnits;;
    }
}
