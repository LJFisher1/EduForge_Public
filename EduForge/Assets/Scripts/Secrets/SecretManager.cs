using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretManager : MonoBehaviour
{
    public List<SecretObject> secretObjects = new List<SecretObject>();  // List of all secret objects
    public Door secretDoor;  // Reference to the secret door

    private int secretsCollected = 0;  // Track the number of collected secrets

    private void Start()
    {
        // Subscribe to the collected event for each secret object
        foreach (SecretObject secret in secretObjects)
        {
            if (secret != null)
            {
                secret.onSecretCollected.AddListener(OnSecretCollected);
            }
        }
    }

    // Called whenever a secret is collected
    private void OnSecretCollected(string secretID)
    {
        secretsCollected++;
        Debug.Log($"Secret {secretID} collected! {secretsCollected}/{secretObjects.Count} secrets collected.");

        // Check if all secrets are collected
        if (secretsCollected >= secretObjects.Count)
        {
            UnlockSecretDoor();
        }
    }

    // Unlock the secret door
    private void UnlockSecretDoor()
    {
        if (secretDoor != null)
        {
            secretDoor.UnlockDoor();
            Debug.Log("All secrets collected! Secret door unlocked.");
        }
    }
}