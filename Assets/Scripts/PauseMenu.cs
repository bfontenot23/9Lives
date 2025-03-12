using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance != null && GameManager.Instance.gameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (GameManager.Instance != null && GameManager.Instance.gameIsPaused == false)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void Resume()
    {
        if (GameManager.Instance != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameManager.Instance.gameIsPaused = false;
            GameManager.Instance.playerControlsEnabled = true;
        }
    }

    void Pause()
    {
        if (GameManager.Instance != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.Instance.gameIsPaused = true;
            GameManager.Instance.playerControlsEnabled = false;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Destroy(GameManager.Instance);
        Destroy(SpawnPoint.Instance);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
