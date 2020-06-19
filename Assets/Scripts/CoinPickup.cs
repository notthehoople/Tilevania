using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] AudioClip coinPickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // We need to play the sound clip at the position of the main camera
        // Otherwise it plays at the position of the coin which is far away
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
