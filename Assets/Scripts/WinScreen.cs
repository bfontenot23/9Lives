using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public void NextLevel()
    {
        Time.timeScale = 1f;
        Destroy(GameManager.Instance);
        Destroy(SpawnPoint.Instance);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Destroy(GameManager.Instance);
        Destroy(SpawnPoint.Instance);
        BGM.Instance.ShutUp();
        Destroy(BGM.Instance);
        SceneManager.LoadScene("MainMenu");
    }
}
