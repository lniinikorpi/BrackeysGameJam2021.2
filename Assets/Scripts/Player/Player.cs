using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int humansCollected;
    public float gravityPerHuman = .5f;
    public Animator anim;
    public AudioSource audioSource;
    public AudioSource shieldAudioSource;
    public AudioClip hitClip;
    public AudioClip humanHitClip;
    public AudioClip shieldOnClip;
    public AudioClip shieldOffClip;
    public GameObject shield;
    public GameObject dieParticles;
    int _currentHealth;
    Rigidbody2D _rb;
    PointEffector2D _pointEffector2D;
    public bool isShieldActive;
    float _shieldTimer;
    float _shieldMaxTime;
    public Camera mainCamera;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointEffector2D = GetComponentInChildren<PointEffector2D>();
        shield.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        shield.GetComponent<CircleCollider2D>().enabled = false;
    }
    void Start()
    {
        GameManager.instance.isPlayerAlive = true;
        GameManager.instance.score = 0;
        _currentHealth = maxHealth;
        UIManagerGame.instance.UpdateHealthSlider(1);
        UIManagerGame.instance.UpdateGravityText(Mathf.Abs((int)_pointEffector2D.forceMagnitude));
        if(!GameManager.instance.isMuted)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isShieldActive)
        {
            ReduceShield();
        }
        if(GameManager.instance.isPlayerAlive)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
        }
        float fps = 1.0f / Time.deltaTime;
        //print(fps);
    }

    public void TakeHit(int value)
    {
        audioSource.clip = hitClip;
        audioSource.Play();
        if (isShieldActive)
        {
            return;
        }
        _currentHealth -= value;
        UIManagerGame.instance.UpdateHealthSlider((float)_currentHealth / (float)maxHealth);
        if(_currentHealth <= 0)
        {
            Die();
        }
        print("oof " + _currentHealth);
    }

    private void Die()
    {
        _currentHealth = 0;
        print("Game over");
        GameManager.instance.isPlayerAlive = false;
        GameObject go = Instantiate(dieParticles);
        go.transform.position = transform.position;
        Destroy(gameObject);
    }

    public void TakeHumans(int value)
    {
        audioSource.clip = humanHitClip;
        audioSource.Play();
        humansCollected += value;
        _currentHealth += value / 10;
        GameManager.instance.score = humansCollected;
        UIManagerGame.instance.UpdateScoreText(humansCollected);
        if(_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        UIManagerGame.instance.UpdateHealthSlider((float)_currentHealth / (float)maxHealth);
        _pointEffector2D.forceMagnitude -= (gravityPerHuman + Random.Range(-.15f,.15f)) * value;
        UIManagerGame.instance.UpdateGravityText((int)Mathf.Abs(_pointEffector2D.forceMagnitude));
        print("yay");
    }

    public void PickPowerUp(PowerUp pu)
    {
        Type type = pu.type;
        switch (type)
        {
            case Type.shield:
                PickShield(pu);
                break;
            default:
                break;
        }
    }

    void PickShield(PowerUp pu)
    {
        if(!isShieldActive)
        {
            audioSource.clip = shieldOnClip;
            audioSource.Play();
        }
        if(!isShieldActive)
        {
            anim.SetBool("ShieldOn", true);
        }
        _shieldMaxTime = pu.shieldTime;
        _shieldTimer = _shieldMaxTime;
        isShieldActive = true;
        shield.GetComponent<CircleCollider2D>().enabled = true;
    }

    void ReduceShield()
    {
        _shieldTimer -= Time.deltaTime;
        if(_shieldTimer <= 0)
        {
            _shieldTimer = 0;
            isShieldActive = false;
            anim.SetBool("ShieldOn", false);
            shield.GetComponent<CircleCollider2D>().enabled = false;
            audioSource.clip = shieldOffClip;
            audioSource.Play();
        }
        float percentage = _shieldTimer / _shieldMaxTime;
        UIManagerGame.instance.UpdateShieldSlider(percentage);
    }
}
