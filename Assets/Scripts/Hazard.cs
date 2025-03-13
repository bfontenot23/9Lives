using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController controller = collision.transform.gameObject.GetComponent<PlayerController>();
            controller.Reset();
        }
    }
}
