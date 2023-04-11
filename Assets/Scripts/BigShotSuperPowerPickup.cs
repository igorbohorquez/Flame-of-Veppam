using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShotSuperPowerPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            other.transform.GetComponent<PlayerMovement>().ActivateBigShotSuperPower();
            Destroy(gameObject);
        }
    }
}
