using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button soundOnButton;
    public Button soundOffButton;
    // Start is called before the first frame update

    private void Awake()
    {
        soundOnButton.gameObject.SetActive(true);
        soundOffButton.gameObject.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
#if UNITY_STANDALONE_WIN
        Application.Quit();
#endif
#if UNITY_WEB
#endif
    }

    public void ToggleMute()
    {
        SoundManager.instance.isMuted = !SoundManager.instance.isMuted;
        soundOffButton.gameObject.SetActive(SoundManager.instance.isMuted);
        soundOnButton.gameObject.SetActive(!SoundManager.instance.isMuted);
    }
}
