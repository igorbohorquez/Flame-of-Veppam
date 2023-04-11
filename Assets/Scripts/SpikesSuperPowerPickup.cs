using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSuperPowerPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerMovement>().ActivateSpikesSuperPower();
            Destroy(gameObject);
        }
    }
}
