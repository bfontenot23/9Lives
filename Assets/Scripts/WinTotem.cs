using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTotem : MonoBehaviour
{
    public GameObject winScreen;
    private AudioSource win;

    private void Start()
    {
        win = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            win.Play();
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
                    switch (GameManager.Instance.loops)
                    {
                        case 2:
                            PlayerPrefs.SetInt("level3Stars", 3);
                            break;
                        case 3:
                            PlayerPrefs.SetInt("level3Stars", 2);
                            break;
                        default:
                            if (GameManager.Instance.loops < 2) PlayerPrefs.SetInt("level3Stars", 3);
                            else PlayerPrefs.SetInt("level3Stars", 1);
                            break;
                    }
                    break;
                case "Level4":
                    switch (GameManager.Instance.loops)
                    {
                        case 1:
                            PlayerPrefs.SetInt("level4Stars", 3);
                            break;
                        case 2:
                            PlayerPrefs.SetInt("level4Stars", 2);
                            break;
                        default:
                            if (GameManager.Instance.loops < 1) PlayerPrefs.SetInt("level4Stars", 3);
                            else PlayerPrefs.SetInt("level4Stars", 1);
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
