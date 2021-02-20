using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance = null;
    [Header("Stats")]
    public int minDistance = 100;
    public int maxDistance = 400;
    public int maxThingCount = 50;
    public int maxStartSpeed = 20;
    public int maxStartRotation = 1;
    public int maxStarCount = 100;
    public int maxPowerUpCount = 10;
    [Header("References")]
    public List<GameObject> spawnables = new List<GameObject>();
    public GameObject starPrefab;
    public GameObject powerUp;
    [HideInInspector]
    public int currentThingCount;
    [HideInInspector]
    public int currentStarCount = 10;
    [HideInInspector]
    public int currentPowerUpCount;
    public GameObject player;
    public Transform spawnsParent;
    public Transform starsParent;
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
            SpawnPowerUp();
            _canSpawn = Time.time + .01f;
        }
    }

    void SpawnThing()
    {
        if (currentThingCount >= maxThingCount)
        {
            return;
        }
        currentThingCount++;
        int index = Random.Range(0, spawnables.Count);
        GameObject go = Instantiate(spawnables[index], spawnsParent);
        AddForces(go);
        MoveToCircle(go);
    }

    private void AddForces(GameObject go)
    {
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(Random.Range(-maxStartSpeed, maxStartSpeed), Random.Range(-maxStartSpeed, maxStartSpeed));
        rb.AddForce(force);
        rb.AddTorque(Random.Range(-maxStartRotation, maxStartRotation));
    }

    void MoveToCircle(GameObject go)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        Vector2 pos = new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;
        pos *= Random.Range(minDistance, maxDistance + 1);
        if (GameManager.instance.isPlayerAlive)
        {
            pos += (Vector2)player.transform.position; 
        }
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

    void SpawnPowerUp()
    {
        if(currentPowerUpCount >= maxPowerUpCount)
        {
            return;
        }
        currentPowerUpCount++;
        GameObject go = Instantiate(powerUp, spawnsParent);
        PowerUp pu = go.GetComponent<PowerUp>();
        pu.type = (Type)Random.Range(0, System.Enum.GetValues(typeof(Type)).Length);
        pu.Initialize();
        AddForces(go);
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
