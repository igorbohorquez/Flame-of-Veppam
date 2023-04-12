using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (other.tag == "Player" && gameSession.getCurrentLife() < 3) {
            gameSession.IncreaseLife();
            Destroy(gameObject);
        }
    }
}
