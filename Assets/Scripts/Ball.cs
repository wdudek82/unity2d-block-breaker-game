using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Ball : MonoBehaviour
{
    // config params
    [SerializeField] private Paddle paddle1;
    [SerializeField] private float xPush = 2f;
    [SerializeField] private float yPush = 12f;
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randomFactor = 0.5f;

    // state
    private bool hasStarted;
    private Vector2 paddleToBallVector;

    // Cached component references
    private AudioSource myAudioSource;
    private Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        var transformBall = transform;
        var ballPosition = transformBall.position;
        paddleToBallVector = ballPosition - paddle1.transform.position;
        transformBall.position = new Vector2(paddleToBallVector.x, ballPosition.y);

        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
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
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
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
        Vector2 velocityTweak = new Vector2(
            Random.Range(-randomFactor, randomFactor),
            Random.Range(-randomFactor, randomFactor));
        AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
        myRigidBody2D.velocity += velocityTweak;
        myAudioSource.PlayOneShot(clip);
    }
}