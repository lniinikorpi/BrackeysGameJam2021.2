using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : SpawnableBase
{
    public int maxMass = 200;
    public int minMass = 50;
    Rigidbody2D _rb;
    Vector2 _startScale;
    public GameObject collisionParticles;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startScale = transform.localScale;
    }
    void Start()
    {
        Initialize();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            int damage = (int)_rb.mass / 10;
            player.TakeHit(damage);
            GameObject go = Instantiate(collisionParticles);
            go.transform.position = collision.GetContact(0).point;
            if (!Spawner.instance.player.GetComponent<Player>().isShieldActive)
            {
                go = Instantiate(UIManagerGame.instance.floatText);
                go.transform.position = collision.GetContact(0).point;
                FloatTextObject f = go.GetComponent<FloatTextObject>();
                f.Initialize(-damage, Color.red); 
            }
            DestroyObject();
        }
    }

    void Initialize()
    {
        int mass = Random.Range(minMass, maxMass + 1);
        _rb.mass = mass;
        transform.localScale = _startScale * ((float)mass /((float)minMass + (((float)maxMass - (float)minMass) / 2)));
    }
}
