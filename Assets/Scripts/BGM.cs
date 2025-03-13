using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShutUp()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        audio.Stop();
    }
}
