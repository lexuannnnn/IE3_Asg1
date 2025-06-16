using UnityEngine;
using TMPro;

public class DoorBehaviour : MonoBehaviour
{
    bool isOpen = false;
    AudioSource doorAudioSource;
    [SerializeField]
    public TextMeshProUGUI doorText;
    public float messageDuration = 2f;
    public bool isLocked = false; // Indicates if the door is locked
    float doorTimer = 0f;

    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
        if (doorText != null)
            doorText.gameObject.SetActive(false); // ensure it's hidden at start
    }
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
    void HideDoorText()
    {
        if (doorText != null)
            doorText.gameObject.SetActive(false);
    }

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