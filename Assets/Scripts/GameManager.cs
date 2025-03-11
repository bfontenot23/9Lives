using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPowerable
{
    public void OnPower();
    public void OnUnpower();
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // List to store input recordings from each run.
    public List<List<PlayerInputRecorder.InputFrame>> pastRuns = new List<List<PlayerInputRecorder.InputFrame>>();

    // Reference to a ghost prefab that has a GhostController component.
    public GameObject ghostPrefab;
    // Reference to a player prefab.
    public GameObject playerPrefab;
    // Transform that represents the spawn point for ghosts and the player.
    public Transform spawnPoint;
    // Radius to check if the spawn area is clear.
    public float spawnAreaRadius = 1f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // This is called every time a scene is loaded.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SpawnEntities());
    }


    // Returns true if no colliders with tags "Player" or "Ghost" are in the spawn area.
    bool IsSpawnAreaClear()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPoint.position, spawnAreaRadius);
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player") || col.CompareTag("Ghost"))
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator WaitForSpawnAreaClear()
    {
        while (!IsSpawnAreaClear())
        {
            yield return null;
        }
    }

    IEnumerator SpawnEntities()
    {
        // Spawn ghosts, each only if the spawn area is clear.
        foreach (var run in pastRuns)
        {
            yield return StartCoroutine(WaitForSpawnAreaClear());
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);
            GhostController ghostController = ghost.GetComponent<GhostController>();
            ghostController.playbackInputs = run;
            yield return new WaitForSeconds(1f);
        }

        // Wait until the spawn area is clear before spawning the player.
        yield return StartCoroutine(WaitForSpawnAreaClear());
        // Optionally, wait 1 second after the last ghost if there were any.
        yield return new WaitForSeconds(1f);
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Call this at the end of a run to store the current recording.
    public void AddRun(List<PlayerInputRecorder.InputFrame> runData)
    {
        // Clone or copy the recording if necessary.
        pastRuns.Add(new List<PlayerInputRecorder.InputFrame>(runData));
    }

    // Regular reset: reloads the level while keeping past runs.
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Hard reset: clears past runs and reloads the level.
    public void HardResetLevel()
    {
        pastRuns.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
