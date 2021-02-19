using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    Rigidbody2D _rb;
    PointEffector2D _pointEffector;
    public ParticleSystem particleSystem;
    [Header("Stats")]
    public float speed = 10;
    public float turnSpeed = 10;
    Vector2 _movement;
    // Start is called before the first frame update

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointEffector = GetComponent<PointEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        if(_movement.y < 0)
        {
            _movement = new Vector2(_movement.x, 0);
        }
        if(_movement.y != 0)
        {
            particleSystem.Play();
        }
        else
        {
            particleSystem.Stop();
        }
    }

    void Move()
    {
        _rb.AddForce(transform.up * speed * _movement.y);
        _rb.AddTorque(turnSpeed * -_movement.x);
    }
}
