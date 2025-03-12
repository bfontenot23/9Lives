using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTotem : MonoBehaviour
{
    public GameObject winScreen;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.playerControlsEnabled = false;
            GameManager.Instance.gameIsPaused = true;
            winScreen.SetActive(true);
            Animator animator = collision.transform.GetChild(0).GetComponent<Animator>();
            animator.SetBool("moving", false);
            animator.SetBool("airborne", false);

            // Save star data
            switch (SceneManager.GetActiveScene().name)
            {
                case "Level1":
                    switch (GameManager.Instance.loops)
                    {
                        case 1:
                            PlayerPrefs.SetInt("level1Stars", 3);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("level1Stars", 2);
                            break;
                        default:
                            if(GameManager.Instance.loops < 1) PlayerPrefs.SetInt("level1Stars", 3);
                            else PlayerPrefs.SetInt("level1Stars", 1);
                            break;
                    }
                    break;
                case "Level2":
                switch (GameManager.Instance.loops)
                    {
                        case 1:
                            PlayerPrefs.SetInt("level2Stars", 3);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("level2Stars", 2);
                            break;
                        default:
                            if(GameManager.Instance.loops < 1) PlayerPrefs.SetInt("level2Stars", 3);
                            else PlayerPrefs.SetInt("level2Stars", 1);
                            break;
                    }
                    break;
                case "Level3":
                    break;
                case "Level4":
                    break;
                default:
                    break;
            }
        }
    }
}
