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
    public AudioClip hitClip;
    public AudioClip humanHitClip;
    public GameObject shield;
    int _currentHealth;
    Rigidbody2D _rb;
    PointEffector2D _pointEffector2D;
    public bool isShieldActive;
    float _shieldTimer;
    float _shieldMaxTime;
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
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShieldActive)
        {
            ReduceShield();
        }
        float msec = Time.deltaTime * 1000.0f;
        float fps = 1.0f / Time.deltaTime;
        print(fps);
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
        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            print("Game over");
            Spawner.instance.isPlayerAlive = false;
            Destroy(gameObject);
        }
        print("oof " + _currentHealth);
    }

    public void TakeHumans(int value)
    {
        audioSource.clip = humanHitClip;
        audioSource.Play();
        humansCollected += value;
        _pointEffector2D.forceMagnitude -= gravityPerHuman * value;
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
        }
        float percentage = _shieldTimer / _shieldMaxTime;
        UIManagerGame.instance.UpdateShieldSlider(percentage);
    }
}
