using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject levelSelectUI;
    public GameObject normalMenu;
    public GameObject resetConfirm;

    public void Play()
    {
        mainMenuUI.SetActive(false);
        levelSelectUI.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        levelSelectUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void Reset()
    {
        normalMenu.SetActive(false);
        resetConfirm.SetActive(true);
    }

    public void ConfirmReset()
    {
        normalMenu.SetActive(true);
        resetConfirm.SetActive(false);
        PlayerPrefs.DeleteAll();
    }

    public void DenyReset()
    {
        normalMenu.SetActive(true);
        resetConfirm.SetActive(false);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene("Level4");
    }
}
