using System.Security.Cryptography;
using UnityEngine;

public class GarageInteraction : MonoBehaviour
{
    // Optional: Add an audio clip to play when the item is collected
    public AudioClip collectSound;
    private AudioSource audioSource;
    private bool isPlayerNearby = false;
    private float disableDuration = 5.0f; // Time in seconds the item will remain disabled
    private float disableTimer = 0.0f;

    void Start()
    {
        // Get the AudioSource component if there is one
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!isPlayerNearby && !gameObject.activeInHierarchy)
        {
            disableTimer += Time.deltaTime;

            if (disableTimer >= disableDuration)
            {
                gameObject.SetActive(true);
                disableTimer = 0.0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object interacting with the item is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;

            // Play the collect sound if available
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }

            // Disable the item from the hierarchy
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object leaving the item area is the player
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
