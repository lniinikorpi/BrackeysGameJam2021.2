using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    shield
}

public class PowerUp : MonoBehaviour
{
    public Type type;
    public SpriteRenderer spriteRenderer;
    public Sprite shieldSprite;
    public float shieldTime;
    private void Update()
    {
        if (Spawner.instance.isPlayerAlive)
        {
            if (Vector2.Distance(transform.position, Spawner.instance.player.transform.position) > Spawner.instance.maxDistance)
            {
                Spawner.instance.currentPowerUpCount--;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Spawner.instance.player.GetComponent<Player>().PickPowerUp(this);
            Spawner.instance.currentPowerUpCount--;
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        switch (type)
        {
            case Type.shield:
                spriteRenderer.sprite = shieldSprite;
                break;
            default:
                break;
        }
    }

}
