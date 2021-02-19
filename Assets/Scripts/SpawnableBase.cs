using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Spawner.instance.isPlayerAlive)
        {
            if (Vector2.Distance(transform.position, Spawner.instance.player.transform.position) > Spawner.instance.maxDistance)
            {
                DestroyObject();
            } 
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
        Spawner.instance.currentThingCount--;
    }

}
