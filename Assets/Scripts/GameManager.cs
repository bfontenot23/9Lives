using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public List<List<PlayerInputRecorder.InputFrame>> pastRuns = new List<List<PlayerInputRecorder.InputFrame>>();
    public GameObject ghostPrefab;
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public float spawnAreaRadius = 0f;
    public bool gameIsPaused = false;
    public int loops = 0;
    public bool playerControlsEnabled = true;

    private AudioSource resetSound;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            resetSound = gameObject.GetComponent<AudioSource>();
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
        foreach (var run in pastRuns)
        {
            if(run != pastRuns.First()) yield return new WaitForSeconds(1f);
            yield return StartCoroutine(WaitForSpawnAreaClear());
            GameObject ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);
            GhostController ghostController = ghost.GetComponent<GhostController>();
            ghostController.playbackInputs = run;
        }

        // Wait until the spawn area is clear before spawning the player.
        if(pastRuns.Count != 0) yield return new WaitForSeconds(1f);
        yield return StartCoroutine(WaitForSpawnAreaClear());
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    // Call this at the end of a run to store the current recording.
    public void AddRun(List<PlayerInputRecorder.InputFrame> runData)
    {
        // Create a copy of the run data.
        List<PlayerInputRecorder.InputFrame> runCopy = new List<PlayerInputRecorder.InputFrame>(runData);

        // Append an extra frame with zero movement.
        PlayerInputRecorder.InputFrame resetFrame = new PlayerInputRecorder.InputFrame();
        // Set relativeTime to a little after the last frame.
        resetFrame.relativeTime = runCopy.Count > 0 ? runCopy[runCopy.Count - 1].relativeTime + Time.fixedDeltaTime : 0f;
        resetFrame.horizontal = 0f;
        resetFrame.jump = false;
        runCopy.Add(resetFrame);

        pastRuns.Add(runCopy);
    }

    // Regular reset: reloads the level while keeping past runs.
    public void ResetLevel()
    {
        loops = loops + 1;
        resetSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Hard reset: clears past runs and reloads the level.
    public void HardResetLevel()
    {
        pastRuns.Clear();
        Time.timeScale = 1f;
        gameIsPaused = false;
        playerControlsEnabled = true;
        loops = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
