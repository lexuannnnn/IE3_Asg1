using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI collectibleText;
    [SerializeField]
    bool canInteract = false;
    // Stores the current collectible/door the player has most recently detected
    CollectibleBehaviour currentCollectible = null;
    DoorBehaviour currentDoor = null;
    public Transform spawnPoint;
    public int currentHealth = 100;
    public int collectibleCount = 0;
    private int newHealth = 0; // Variable to store the new health value
    void Start()
    {
        healthText.text = "Health " + currentHealth.ToString();
        collectibleText.text = "Collectibles collected " + collectibleCount.ToString() + " / 10";
    }

    public void ModifyHealth(int damage)
    {
        newHealth = currentHealth -= damage;

        if (healthText != null)
        {
            healthText.text = "Health " + newHealth.ToString();
        }
}
    public void ModifyCount(int collectibleScore)
    {
        collectibleCount += collectibleScore;
        collectibleText.text = "Collectibles collected " + collectibleCount.ToString() + " / 10";
    }

    // Trigger Callback for when the player enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("Collectible"))
        {
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

    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected collectible or door
        if (currentCollectible != null)
        {
            canInteract = false;
            currentCollectible = null;
        }
        if (currentDoor != null)
        {
            canInteract = false;
            currentDoor = null;
            OnInteract(); // Automatically interact with the door when exited

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
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 5f, Color.red);
        // Check if the player is pressing the interact key (e.g., "E")
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, 5f))
        {
            // Check if the raycast is hitting an object with the "Collectible" tag
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
        }
        // For when the raycast is not hitting any object
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
