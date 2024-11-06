using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecretObject : MonoBehaviour
{
    public UnityEvent<string> onSecretCollected;  // Event for when the secret is collected
    public string secretID;  // Unique ID for the secret object
    private bool isCollected = false;  // Track if the secret is collected
    private bool isPlayerNear = false;  // Track if the player is near the secret

    private void Start()
    {
        if (onSecretCollected == null)
        {
            onSecretCollected = new UnityEvent<string>();
        }
    }

    private void Update()
    {
        // Check if the player is near and presses the interact key
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            CollectSecret();
        }
    }

    // Call this method when the secret is collected
    private void CollectSecret()
    {
        if (!isCollected)
        {
            isCollected = true;
            onSecretCollected.Invoke(secretID);  // Notify the manager
            Debug.Log("Secret " + secretID + " collected!");
            gameObject.SetActive(false);  // Disable the object after collection
        }
    }

    // Detect player entering the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Check if the player enters
        {
            isPlayerNear = true;
            Debug.Log("Player near secret " + secretID);
        }
    }

    // Detect player leaving the trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left secret " + secretID);
        }
    }
}