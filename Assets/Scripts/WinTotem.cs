using UnityEngine;

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
        }
    }
}
