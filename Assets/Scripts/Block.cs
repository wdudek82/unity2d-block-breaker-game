using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private AudioClip breakSound;

    // Cached reference
    Level level;
    private GameStatus gameStatus;

    public void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameStatus.AddToScore();
        DestroyBlock();
    }

    private void DestroyBlock()
    {
        if (Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(breakSound, Camera.main.gameObject.transform.position);
        }

        Destroy(gameObject);
        level.BlockDestroyed();
    }
}
