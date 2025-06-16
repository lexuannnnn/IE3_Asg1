using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    AudioSource doorAudioSource;
    void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }

    bool isOpen = false;
    public void Interact()
    {
        if (isOpen == true)
        {
            doorAudioSource.Play();
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
            isOpen = false;
        }

        else if (isOpen == false)
        {
            doorAudioSource.Play();
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y -= 90f;
            transform.eulerAngles = doorRotation;
            isOpen = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
            Vector3 doorRotation = transform.eulerAngles;
            doorRotation.y += 90f;
            transform.eulerAngles = doorRotation;
    }
}