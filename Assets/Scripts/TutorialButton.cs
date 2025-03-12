using UnityEngine;
using UnityEngine.Playables;

public class TutorialButton : MonoBehaviour
{

    public PlayableDirector tutorialButton;
    private int buttonPresses = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.loops == 0)
        {
            buttonPresses++;
            if (buttonPresses == 6)
            {
                tutorialButton.Play();
            }

        }
    }
}
