using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerGame : MonoBehaviour
{
    public static UIManagerGame instance = null;
    public Slider shieldSlider;
    public Slider healthSlider;
    public GameObject pausePanel;
    public GameObject endPanel;
    public AudioSource audioSource;
    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    public Animator anim;
    public GameObject floatText;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        shieldSlider.value = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShieldSlider(float value)
    {
        shieldSlider.value = value;
    }

    public void UpdateHealthSlider(float value)
    {
        Image image = healthSlider.GetComponentsInChildren<Image>()[1];
        healthSlider.value = value;
        if (healthSlider.value > .5f)
        {
            image.color = new Color(2 - 2 * value, 1, 0);
        }
        else if (healthSlider.value <= .5f)
        {
            image.color = new Color(1, (2 * value), 0);
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        pausePanel.SetActive(GameManager.instance.isPaused);
        if(GameManager.instance.isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void EndGame()
    {
        endPanel.SetActive(true);
        endScoreText.text = scoreText.text;
    }

    public void UpdateScoreText(int value)
    {
        scoreText.text = value.ToString();
        anim.SetTrigger("Score");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
