using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    PlayerMovement player;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy (other.gameObject);
        }
        Destroy (gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy (gameObject);
    }
}
