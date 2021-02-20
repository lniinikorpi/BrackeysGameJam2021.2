using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    Rigidbody2D _rb;
    PointEffector2D _pointEffector;
    public ParticleSystem mainMotorParticles;
    public ParticleSystem rightMotorParticles;
    public ParticleSystem leftMotorParticles;
    public GameObject mainMotorFire;
    public GameObject rightMotorFire;
    public GameObject leftMotorFire;
    public AudioSource mainMotorAudio;
    public AudioSource rightMotorAudio;
    public AudioSource leftMotorAudio;
    [Header("Stats")]
    public float speed = 10;
    public float turnSpeed = 10;
    Vector2 _movement;
    // Start is called before the first frame update

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pointEffector = GetComponent<PointEffector2D>();
        mainMotorFire.SetActive(false);
        rightMotorFire.SetActive(false);
        leftMotorFire.SetActive(false);
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
            mainMotorParticles.Play();
            mainMotorFire.SetActive(true);
            mainMotorAudio.enabled = true;
        }
        else
        {
            mainMotorParticles.Stop();
            mainMotorFire.SetActive(false);
            mainMotorAudio.enabled = false;
        }
        if(_movement.x > 0)
        {
            leftMotorParticles.Play();
            leftMotorFire.SetActive(true);
            leftMotorAudio.enabled = true;
        }
        else if(_movement.x < 0)
        {
            rightMotorParticles.Play();
            rightMotorFire.SetActive(true);
            rightMotorAudio.enabled = true;
        }
        else
        {
            leftMotorParticles.Stop();
            rightMotorParticles.Stop();
            rightMotorFire.SetActive(false);
            leftMotorFire.SetActive(false);
            rightMotorAudio.enabled = false;
            leftMotorAudio.enabled = false;
        }
    }

    void Move()
    {
        _rb.AddForce(transform.up * speed * _movement.y);
        _rb.AddTorque(turnSpeed * -_movement.x);
    }
}
