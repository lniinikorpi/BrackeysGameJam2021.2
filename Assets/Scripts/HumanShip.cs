using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanShip : SpawnableBase
{
    public int maxHumanCount = 100;
    public int minHumanCount = 20;
    int _humanCount;
    public GameObject collisionParticles;
    // Start is called before the first frame update
    void Start()
    {
        InitializeShip();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            player.TakeHumans(_humanCount);
            GameObject go = Instantiate(collisionParticles);
            go.transform.position = collision.GetContact(0).point;
            go = Instantiate(UIManagerGame.instance.floatText);
            go.transform.position = collision.GetContact(0).point;
            FloatTextObject f = go.GetComponent<FloatTextObject>();
            f.Initialize(_humanCount, Color.yellow);
            DestroyObject();
        }
    }

    void InitializeShip()
    {
        _humanCount = Random.Range(minHumanCount, maxHumanCount + 1);
    }
}
