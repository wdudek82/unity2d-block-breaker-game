using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 12f;
    [SerializeField] private AudioClip[] ballSounds;

    // state
    private bool hasStarted;
    private Vector2 paddleToBallVector;
    
    // Cached component references
    private AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        var transformBall = transform;
        var ballPosition = transformBall.position;
        paddleToBallVector = ballPosition - paddle1.transform.position;
        transformBall.position = new Vector2(paddleToBallVector.x, ballPosition.y);

        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasStarted) return;
        LockBallToPaddle();
        LaunchOnClick();
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddleCachedPosition = paddle1.transform.position;
        var paddlePos = new Vector2(paddleCachedPosition.x, paddleCachedPosition.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hasStarted) return;
        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        myAudioSource.PlayOneShot(clip);
    }
}