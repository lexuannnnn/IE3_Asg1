/*
* Author: Tan Le Xuan
* Date: 17/06/25
* Description: Manages player movement, input, and interactions with the game world.
*/

using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    [SerializeField]
    TextMeshProUGUI collectibleText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private GameObject winScreen;
    bool canInteract = false;
    CollectibleBehaviour currentCollectible = null; // Stores the current collectible the player has most recently detected
    DoorBehaviour currentDoor = null; // Stores the current door the player has most recently detected
    KeyBehaviour currentKey = null; // Stores the current key the player has detected
    public Transform spawnPoint;
    private int currentHealth = 100;
    private int collectibleCount = 0;
    public bool hasKey = false; // Indicates if the player has a key for locked doors
    /// <summary>
    /// Initializes the player behaviour.
    /// Sets up the health and collectible text displays.
    /// Sets the interact text to be hidden at the start.
    /// </summary>
    void Start()
    {
        healthText.text = "Health " + currentHealth.ToString();
        collectibleText.text = "Collectibles collected " + collectibleCount.ToString() + " / 10";
        if (interactText != null)
            interactText.gameObject.SetActive(false); // Ensure the interact text is hidden at start
    }
    /// <summary>
    /// Modifies the player's health.
    /// Reduces health by the specified damage amount.
    /// </summary>
    /// <param name="damage"></param>
    public void ModifyHealth(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health " + currentHealth.ToString();
    }
    /// <summary>
    /// Modifies the player's collectible count.
    /// Increments the collectible count by the specified value.
    /// If the collectible count reaches 10, it shows the win screen and disables player movement.
    /// </summary>
    /// <param name="collectibleScore"></param>
    public void ModifyCount(int collectibleScore)
    {
        collectibleCount += collectibleScore;
        collectibleText.text = "Collectibles collected " + collectibleCount.ToString() + " / 10";
        if (collectibleCount >= 10)
        {
            winScreen.SetActive(true); // Show the win screen when 10 collectibles are collected
            GetComponent<PlayerInput>().enabled = false; // Disable player movement
        }
    }

    /// <summary>
    /// Handles the interaction when the player enters a trigger collider.
    /// Detects if the player is near a collectible, door, or key.
    /// </summary>
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
        else if (other.CompareTag("Key"))
        {
            currentKey = other.GetComponent<KeyBehaviour>();
        }
    }
    /// <summary>
    /// Handles the interaction when the player exits a trigger collider.
    /// Unhighlights the collectible or key if the player moves away from it.
    /// </summary>
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
            else if (other.CompareTag("Key") && currentKey != null)
            {
                currentKey.Unhighlight();
                currentKey = null;
            }
        }
    }
    /// <summary>
    /// Updates the player's interaction state.
    /// Checks for raycast hits to determine if the player can interact with collectibles, doors, or keys.
    /// If the player is near a collectible, door, or key, it highlights the object and shows the interact text.
    /// If the player presses the interact key (e.g., "E"), it collects the collectible, interacts with the door, or collects the key.
    /// </summary>
    void Update()
    {
        RaycastHit hitInfo;
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 5f, Color.red);
        // Check if the player is pressing the interact key (e.g., "E")
        bool interactable = false;
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
                interactable = true;
            }
            else if (hitObject.CompareTag("Door"))
            {
                currentDoor = hitObject.GetComponent<DoorBehaviour>();
                interactable = true;
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
                interactable = true;
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
        if (interactText != null)
        {
            interactText.gameObject.SetActive(interactable);
            interactText.text = "Press [E] to Interact";
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
            if (interactText != null)
                interactText.gameObject.SetActive(false);
        }
    }
}
