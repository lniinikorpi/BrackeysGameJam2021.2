using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShip : SpawnableBase
{
    public int maxHumanCount = 100;
    public int minHumanCount = 20;
    int _humanCount;
    // Start is called before the first frame update
    void Start()
    {
        InitializeShip();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().TakeHumans(_humanCount);
            DestroyObject();
        }
    }

    void InitializeShip()
    {
        _humanCount = Random.Range(minHumanCount, maxHumanCount + 1);
    }
}
