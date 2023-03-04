using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerPickup : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerMovement>().ActivateSuperPower();
            Destroy(gameObject);
        }
    }
}
