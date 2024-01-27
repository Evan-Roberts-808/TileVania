using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSound;
    [SerializeField] int pointsForCoinPickup = 100;

    bool wasCollected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().AddtoScore(pointsForCoinPickup);
            AudioSource.PlayClipAtPoint(coinPickupSound, Camera.main.transform.position, 0.2f);
            Destroy(gameObject);
        }
    }
}
