using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private GameObject blockSparklesVfx;
    [SerializeField] private int maxHits;
    [SerializeField] private Sprite[] hitSprites;

    // cached reference
    Level level;
    private GameSession gameSession;

    // state variables
    [SerializeField] private int timesHit; // TODO: Only serialized for debug purposes

    public void Start()
    {
        level = FindObjectOfType<Level>();
        gameSession = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (CompareTag("Unbreakable")) return;
        level.CountBlocks();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }

        ;
        PlayBlockSfx();
    }

    private void HandleHit()
    {
        timesHit++;
//        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            gameSession.AddToScore();
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.Log("Block sprite is missing from array: " + gameObject.name + " " + gameObject.GetHashCode());
        }
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        level.BlockDestroyed();
        TrigerSparklesVfx();
    }

    private void PlayBlockSfx()
    {
        if (Camera.main == null) return;
        AudioClip clip = hitSound;
        if (timesHit >= maxHits)
        {
            clip = breakSound;
        }

        AudioSource.PlayClipAtPoint(clip, Camera.main.gameObject.transform.position);
    }

    private void TrigerSparklesVfx()
    {
        Transform transformCached = transform;
        GameObject blockSparklesVfxInstance =
            Instantiate(blockSparklesVfx, transformCached.position, transformCached.rotation);
        Destroy(blockSparklesVfxInstance, 1f);
    }
}