using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeShotsSuperPowerPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerMovement>().ActivateThreeShotsSuperPower();
            Destroy(gameObject);
        }
    }
}
