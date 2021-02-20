using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPlayerAlive)
        {
            if (Vector2.Distance(transform.position, Spawner.instance.player.transform.position) > Spawner.instance.maxDistance)
            {
                Spawner.instance.currentStarCount--;
                Destroy(gameObject);
            }
        }
    }
}
