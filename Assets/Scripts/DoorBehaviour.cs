/*
* Author: Tan Le Xuan
* Date: 17/06/25
* Description: Controls the door's opening and closing when the player is nearby or meets certain conditions.
*/

using UnityEngine;
using TMPro;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI doorText;
    AudioSource doorAudioSource;
    public float messageDuration = 2f; // Duration to show the message when the door is locked
    public bool isLocked = false; // Indicates if the door is locked
    float doorTimer = 0f; // Timer to track how long the door has been open
    bool isOpen = false; 
    
    /// <summary>
    /// Make the door text not visible at the start
    /// </summary>
    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
        if (doorText != null)
            doorText.gameObject.SetActive(false); // ensure it's hidden at start
    }

    /// <summary>
    /// Handles the interaction when the player interacts with the door.
    /// If the door is locked, it checks if the player has a key.
    /// If the player has a key, the door opens.
    /// If the player does not have a key, it shows a message.
    /// If the door is locked and the player has a key, it opens the door.
    /// If the door is already open, it closes the door.
    /// If the door is already close, it opens the door.
    /// </summary>
    public void Interact()
    {
        if (isOpen == false)
        {
            if (isLocked)
            {
                PlayerBehaviour player = GameObject.FindWithTag("Player")?.GetComponent<PlayerBehaviour>();
                if (player.hasKey)
                {
                    doorAudioSource.Play();
                    Vector3 doorRotation = transform.eulerAngles;
                    doorRotation.y -= 90f;
                    transform.eulerAngles = doorRotation;
                    isOpen = true;
                    doorTimer = 0f;
                }
                else
                {
                    if (doorText != null)
                        doorText.gameObject.SetActive(true);
                    Invoke(nameof(HideDoorText), messageDuration);
                }
            }
            else
            {
                doorAudioSource.Play();
                Vector3 doorRotation = transform.eulerAngles;
                doorRotation.y -= 90f;
                transform.eulerAngles = doorRotation;
                isOpen = true;
                doorTimer = 0f; // Reset timer when door is opened
            }
        }

        else if (isOpen == true)
        {
            doorAudioSource.Play();
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
        }
    }

    /// <summary>
    /// Hides the door text after a specified duration.
    /// </summary>
    void HideDoorText()
    {
        if (doorText != null)
            doorText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the door's state every frame.
    /// If the door is open, it checks if the timer has reached the specified duration.
    /// If so, it closes the door and resets the timer.
    /// </summary>
    void Update()
    {
        if (isOpen)
    {
        doorTimer += Time.deltaTime;
        if (doorTimer >= 2f)
        {
            doorAudioSource.Play();
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
            doorTimer = 0f;
        }
    }
    }
}