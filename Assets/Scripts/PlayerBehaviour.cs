using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    bool canInteract = false;
    // Stores the current coin object the player has detected
    CollectibleBehaviour currentCollectible = null;
    DoorBehaviour currentDoor = null;
    public Transform spawnPoint;

    // Trigger Callback for when the player enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("Collectible"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            canInteract = true;
            currentCollectible = other.GetComponent<CollectibleBehaviour>();
            currentCollectible.Collect();
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
            OnInteract(); // Automatically interact with the door when detected
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected coin or door
        if (currentCollectible != null)
        {
            canInteract = false;
            currentCollectible = null;
        }
        if (currentDoor != null)
        {
            canInteract = false;
            currentDoor = null;

        }
    }

    void OnInteract()
    {
        if (currentCollectible != null)
        {
            // Call the Collect method on the coin object
            // Pass the player object as an argument
            currentCollectible.Collect();
        }
        if (currentDoor != null)
        {
            // Call the Interact method on the door object
            // This allows the player to open or close the door
            currentDoor.Interact();
        }
    }
    void Update()
    {
        RaycastHit hitInfo;
        // Check if the player is pressing the interact key (e.g., "E")
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 5f, Color.red);
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, 5f))
        {
            if (hitInfo.collider.gameObject.CompareTag("Collectible"))
            {
                if (currentCollectible != null)
                {
                    currentCollectible.Unhighlight();
                    currentCollectible = null;
                }
                canInteract = true;
                currentCollectible = hitInfo.collider.gameObject.GetComponent<CollectibleBehaviour>();
                currentCollectible.Highlight();
            }

            else
            {
                if (currentCollectible != null)
                {
                    currentCollectible.Unhighlight();
                    currentCollectible = null;
                    canInteract = false;
                }
            }
        }
        else
        {
            if (currentCollectible != null)
            {
                currentCollectible.Unhighlight();
                currentCollectible = null;
                canInteract = false;
            }
        }
    }
}
