using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button soundOnButton;
    public Button soundOffButton;
    public AudioSource audioSource;

    private void Start()
    {
        soundOnButton.gameObject.SetActive(!GameManager.instance.isMuted);
        soundOffButton.gameObject.SetActive(GameManager.instance.isMuted);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
#if UNITY_ANDROID
        Application.Quit();
#endif
#if UNITY_WEB
#endif
    }

    public void ToggleMute()
    {
        GameManager.instance.isMuted = !GameManager.instance.isMuted;
        soundOffButton.gameObject.SetActive(GameManager.instance.isMuted);
        soundOnButton.gameObject.SetActive(!GameManager.instance.isMuted);
        if (GameManager.instance.isMuted)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }
}
