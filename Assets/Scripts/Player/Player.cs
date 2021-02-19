using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int humansCollected;
    public float gravityPerHuman = .5f;
    int _currentHealth;
    Rigidbody2D _rb;
    PointEffector2D _pointEffector2D;
    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointEffector2D = GetComponentInChildren<PointEffector2D>();
    }
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeHit(int value)
    {
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
        humansCollected += value;
        _pointEffector2D.forceMagnitude -= gravityPerHuman * value;
        print("yay");
    }
}
