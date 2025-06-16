using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI collectibleText;
    bool canInteract = false;
    // Stores the current collectible/door the player has most recently detected
    CollectibleBehaviour currentCollectible = null;
    DoorBehaviour currentDoor = null;
    KeyBehaviour currentKey = null; // Stores the current key the player has detected
    public Transform spawnPoint;
    private int currentHealth = 100;
    private int collectibleCount = 0;
    public bool hasKey = false; // Indicates if the player has a key for locked doors

    void Start()
    {
        healthText.text = "Health " + currentHealth.ToString();
        collectibleText.text = "Collectibles collected " + collectibleCount.ToString() + " / 10";
    }

    public void ModifyHealth(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health " + currentHealth.ToString();
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
            currentCollectible = other.GetComponent<CollectibleBehaviour>();
        }
        else if (other.CompareTag("Door"))
        {
            currentDoor = other.GetComponent<DoorBehaviour>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected collectible or door
        {
            if (other.CompareTag("Collectible") && currentCollectible != null)
            {
                currentCollectible.Unhighlight();
                currentCollectible = null;
            }
            else if (other.CompareTag("Door"))
            {
                currentDoor = null;
            }
        }
    }
    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 5f, Color.red);
        // Check if the player is pressing the interact key (e.g., "E")
        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hitInfo, 5f))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            // Check if the raycast is hitting an object with the "Collectible" tag
            if (hitObject.CompareTag("Collectible"))
            {
                CollectibleBehaviour collectible = hitObject.GetComponent<CollectibleBehaviour>();
                if (currentCollectible != collectible)
                {
                    if (currentCollectible != null) currentCollectible.Unhighlight();
                    currentCollectible = collectible;
                    currentCollectible.Highlight();
                }
            }
            else if (hitObject.CompareTag("Door"))
            {
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
            }
            else if (hitObject.CompareTag("Key"))
            {
                KeyBehaviour key = hitObject.GetComponent<KeyBehaviour>();
                if (currentKey != key)
                {
                    if (currentKey != null) currentKey.Unhighlight();
                    currentKey = key;
                    currentKey.Highlight();
                }
            }
            else
            {
                if (currentCollectible != null)
                {
                    currentCollectible.Unhighlight();
                    currentCollectible = null;
                }
                currentDoor = null;
            }
        }
        // For when the raycast is not hitting any object
        else
        {
            if (currentCollectible != null)
            {
                currentCollectible.Unhighlight();
                currentCollectible = null;
            }
            currentDoor = null;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentCollectible != null)
            {
                currentCollectible.Collect();
                currentCollectible = null;
            }
            if (currentDoor != null)
            {
                currentDoor.Interact();
            }
            if (currentKey != null)
            {
                currentKey.Collect(this);
                currentKey = null;
            }   
        }
    }
}
