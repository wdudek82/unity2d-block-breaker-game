using UnityEngine;

public class Level : MonoBehaviour
{
    // Parameters
    [SerializeField] private int breakableBlocks;
    
    // Cached reference
    private SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks()
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks -= 1;

        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}