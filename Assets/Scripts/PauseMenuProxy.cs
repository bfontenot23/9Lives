using UnityEngine;

public class PauseMenuProxy : MonoBehaviour
{
    public void Reset()
    {
        GameManager.Instance.HardResetLevel();
    }
}
