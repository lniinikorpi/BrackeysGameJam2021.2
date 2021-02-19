using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableBase : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;

    // Update is called once per frame
    void Update()
    {
        if(player == null && Spawner.instance.isPlayerAlive)
        {
            player = Spawner.instance.player.GetComponent<Player>();
        }
        if (Spawner.instance.isPlayerAlive)
        {
            if (Vector2.Distance(transform.position, player.gameObject.transform.position) > Spawner.instance.maxDistance)
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
