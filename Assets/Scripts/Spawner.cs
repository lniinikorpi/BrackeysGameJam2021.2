using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance = null;
    public int minDistance = 100;
    public int maxDistance = 400;
    public int maxThingCount = 50;
    public List<GameObject> spawnables = new List<GameObject>();
    public GameObject starPrefab;
    public int maxStarCount = 100;
    [HideInInspector]
    public int currentThingCount;
    [HideInInspector]
    public int currentStarCount = 10;
    public GameObject player;
    public Transform spawnsParent;
    public Transform starsParent;
    public bool isPlayerAlive = true;
    float _canSpawn;

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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _canSpawn)
        {
            SpawnThing();
            SpawnStar();
            _canSpawn = Time.time + .2f;
        }
    }

    void SpawnThing()
    {
        if(currentThingCount >= maxThingCount)
        {
            return;
        }
        currentThingCount++;
        int index = Random.Range(0, spawnables.Count);
        GameObject go = Instantiate(spawnables[index], spawnsParent);
        MoveToCircle(go);
    }

    void MoveToCircle(GameObject go)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 pos = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;
        pos *= Random.Range(minDistance, maxDistance + 1);
        pos += (Vector2)player.transform.position;
        go.transform.position = pos;
    }

    void SpawnStar()
    {
        if(currentStarCount >= maxStarCount)
        {
            return;
        }
        currentStarCount++;
        GameObject go = Instantiate(starPrefab, starsParent);
        MoveToCircle(go);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, minDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position, maxDistance);
    }
}
